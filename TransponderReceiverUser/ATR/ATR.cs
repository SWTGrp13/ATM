using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TransponderReceiver;
using TransponderReceiverUser.Calculations;
using TransponderReceiverUser.ObserverPattern;

namespace TransponderReceiverUser.ATR
{
    public class Tower : IAtr
    {
        private Subject ATM;

        private bool isInvalidSpace(Plane sub)
        {
            if (((int)sub.XPos < 0) || ((int)sub.XPos > 80000))
            {
                return false;
            }
            if (((int)sub.YPos < 0) || ((int)sub.YPos > 80000))
            {
                return false;
            }
            if (((int)sub.Altitude <= 500) || ((int)sub.YPos >= 20000))
            {
                return false;
            }
            if ((DateTime.Now.Subtract(sub.TimeStamp).TotalSeconds) >= 2)
            {
                return false;
            }

            return true;
        }

        private void CalculateMetrixes(Plane CurrentPlane)
        {
            
            int Xpos = CurrentPlane.XPos;
            int Ypos = CurrentPlane.YPos;
            int[] CurrentPlaneCoordinates = new int[2];
            CurrentPlaneCoordinates[0] = Xpos;
            CurrentPlaneCoordinates[1] = Ypos;
            
            var planes = ATM.getInstances();

                for (int i = 0; i < planes.Count; i++)
                {
                    var plane = planes[i] as Plane;
                    int[] OtherPlaneCoordinates = {plane.XPos,plane.YPos};
                    double xLength = CurrentPlaneCoordinates[0] - OtherPlaneCoordinates[0];
                    double yLength = CurrentPlaneCoordinates[1] - OtherPlaneCoordinates[1];
                    double HorizontalVector = Math.Sqrt((Math.Pow(xLength,2)) + (Math.Pow(yLength,2)));
                    double HeightVector = CurrentPlane.Altitude - plane.Altitude;


                    if (HorizontalVector <= 5000 && HeightVector <= 300)
                    {
                      Console.WriteLine("The plane " + plane.Tag + " And" + plane.Tag + " is coliding!!!");
                    }
                    
                }
       
            

        }

        public Action onData(RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                var plane = Factory.CreatePlane(data);
                if (isInvalidSpace(plane))
                {
                    if (!ATM.attach(plane))
                    {
                        plane = ATM.getInstance(plane.Tag) as Plane;
                        plane.Update(data);
                    }
                    // loop over all planes to check for hanging data..
                    var inst = ATM.getInstances().ToList();
                    lock (inst) { 
                        foreach (var p in inst) {
                            var pne = p as Plane;
                            if (!isInvalidSpace(pne)) {
                                ATM.detach(pne);
                            }
                        }
                    }


                    CalculateMetrixes(plane);
                }
                else
                {
                    ATM.detach(plane);
                }
            }
            Console.Clear();
            ATM.notify("print");

            return null;
        }

        public void Start()
        {
            ATM = new Subject();
        }
    }
}
