using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverLib.Log;
using TransponderReceiverLib.Tower;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib
{
    public static class Factory
    {
        public static FileConfig GetFileCofig(string FileName,string FilePath)
        {
             return new FileConfig(FileName,FilePath);
        }

        public static FlightLog GetFlightLog(FileConfig cfg)
        {
            return new FlightLog(cfg);
        }

        public static Plane GetPlane(string EncodedMessage)
        {
            return new Plane(EncodedMessage);
        }

        public static AirTrafficTower GetTower(FlightLog log, Subject Subject)
        {
            return new AirTrafficTower(log, Subject);
        }

        public static Subject GetSubject()
        {
            return new Subject();
        }
    }
}
