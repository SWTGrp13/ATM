using System;
using TransponderReceiver;
using TransponderReceiverUser.ObserverPattern;

namespace TransponderReceiverUser
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;
        private Subject ATM = new Subject();

        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        public bool isInvalidSpace(Plane sub)
        {
            if ((int)sub.XPos < 0 || (int)sub.XPos > 80000)
            {
                return false;
            }
            if ((int)sub.YPos < 0 || (int)sub.YPos > 80000)
            {
                return false;
            }
            if ((int)sub.Altitude <= 500 || (int)sub.YPos >= 20000)
            {
                return false;
            }

            return true;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {
                var plane = Factory.CreatePlane(data);
                if (isInvalidSpace(plane))
                {
                    if (!ATM.attach(plane))
                    {
                        plane = ATM.getInstance(plane.Tag) as Plane;
                        plane.Update(data);
                        if (!isInvalidSpace(plane))
                        {
                            ATM.detach(plane);
                        }
                    }
                }
            }
            Console.Clear();
            ATM.notify("print");
        }
    }
}
