using System;
using TransponderReceiver;
using TransponderReceiverUser.ATR;
using TransponderReceiverUser.ObserverPattern;

namespace TransponderReceiverUser
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;

        private Tower _Tower;
                   
        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver, Tower _tower)
        {
            _Tower = _tower;
            _Tower.Start();
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;
            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        
        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            _Tower.onData(e);
        }
    }
}
