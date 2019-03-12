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

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {
                var plane = Factory.CreatePlane(data);
                if (!ATM.attach(plane))
                {
                    ATM.getInstance(plane.Tag).Update(data);
                }
            }
            Console.Clear();
            ATM.notify("print");
        }
    }
}
