using System;
using System.Globalization;

namespace TransponderReceiverUser
{
    public class Plane : IPlane
    {
        public string Tag { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

        public Plane(string data)
        {
            var planeData = data.Split(';');
            Tag = planeData[0];
            XPos = Convert.ToInt32(planeData[1]);
            YPos = Convert.ToInt32(planeData[2]);
            Altitude = Convert.ToInt32(planeData[3]);
            TimeStamp = DateTime.ParseExact(planeData[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
        }
    }
}
