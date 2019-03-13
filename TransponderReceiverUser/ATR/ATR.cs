using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (DateTime.Now.Subtract(sub.TimeStamp).TotalSeconds >= 2)
            {
                return false;
            }

            return true;
        }

        private void CalculateMetrixes(Plane CurrentPlane)
        {
            // todo : calculate flight metrix here.
            // Queue all planes
            var planes = ATM.getInstances();
           
            foreach (var current in planes)
            {
                var plane = current as Plane;

                Console.WriteLine($"calculating metrics for {plane.Tag}");
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
