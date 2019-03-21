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

        private static AirTrafficTower TrafficMonitor;
        private static FileConfig cfg;
        private static FlightLog Log;
        private static Subject TrackList;

        static void Main(string[] args)
        {
            /*
            FlightLog log = new FlightLog(new FileConfig("test.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));
               
            log.Write(LogLevel.WARNING, "warning..");
            log.Write(LogLevel.CRITICAL, "danger..");
            log.Write(LogLevel.NORMAL, "normal..");
            */

            cfg = Factory.GetFileCofig("AirTrafficMonitorLog.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            Log = Factory.GetFlightLog(cfg);

            TrackList = Factory.GetSubject();

            TrafficMonitor = new AirTrafficTower(Log, TrackList,true);

            receiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);

        }
        static void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Console.Clear();
            foreach (var data in e.TransponderData)
            {
                TrafficMonitor.Add(data);
            }
            TrafficMonitor.Render();
        }

    }
}
