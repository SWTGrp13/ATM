using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;
using TransponderReceiverLib;
using TransponderReceiverLib.Log;
using TransponderReceiverLib.Tower;

namespace Monitor
{
    class Program
    {
        static ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
        private static MonitorSystem Sys_;

        static void Main(string[] args)
        {

            Sys_ = new MonitorSystem();
         
            receiver.TransponderDataReady += Sys_.ReceiverOnTransponderDataReady;

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);

        }
       

    }
}
