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
        private static List<CollisionTracker> tracker;
        static void Main(string[] args)
        {

            tracker = Factory.GetTracker();

            cfg = Factory.GetFileConfig("AirTrafficMonitorLog.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            Log = Factory.GetFlightLog(cfg);

            TrackList = Factory.GetSubject();

            TrafficMonitor = new AirTrafficTower(Log, TrackList, tracker,true);

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
            TrafficMonitor.CollisionValidate();
        }

    }
}
