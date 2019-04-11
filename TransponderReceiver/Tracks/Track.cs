using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverLib.Calculations;

namespace TransponderReceiverLib.Tracks
{
    public interface ITrack : IObserver
    {
        string Tag { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }
        int OldYPos { get; set; }
        int OldXPos { get; set; }
        int Altitude { get; set; }
        double Velocity { get; set; }
        double Degrees { get; set; }
        bool ConditionCheck { get; set; }
        DateTime TimeStamp { get; set; }
        DateTime OldTimeStamp { get; set; }
        //bool isInValidSpace();
    }

    public class Track : ITrack
    {
        public string Tag { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int OldYPos { get; set; }
        public int OldXPos { get; set; }
        public int Altitude { get; set; }
        public double Velocity { get; set; }
        public double Degrees { get; set; }
        public bool ConditionCheck { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime OldTimeStamp { get; set; }

        private void ParseData(string data)
        {
            try
            {
                var planeData = data.Split(';');
                Tag = planeData[0];
                XPos = Convert.ToInt32(planeData[1]);
                YPos = Convert.ToInt32(planeData[2]);
                Altitude = Convert.ToInt32(planeData[3]);
                TimeStamp = DateTime.ParseExact(planeData[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                Velocity = new Calculate().FindVelocity(this);
                Degrees = new Calculate().FindDegree(this);
            }
            catch  
            {
              //  throw new PlaneDataException(1, "Invalid Data String: " + e.Message);
            }
        }

        public Track(string data)
        {
            ParseData(data);
        }

        public void Update(string data)
        {
            OldYPos = YPos;
            OldXPos = XPos;
            OldTimeStamp = TimeStamp;
            ParseData(data);
        }

        public string Identify()
        {
            return Tag;
        }
    }
}
