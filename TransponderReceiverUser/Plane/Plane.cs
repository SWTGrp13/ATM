﻿using System;
using System.Globalization;

namespace TransponderReceiverUser
{
    public class PlaneDataException : Exception
    {
        public PlaneDataException(int severity, string message) : base(message)
        {
            
        }
    }

    public class Plane : IPlane
    {
        public string Tag { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }

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
            }
            catch (Exception e)
            {
                throw new PlaneDataException(1, "Invalid Data String: " + e.Message);
            }
        }

        public Plane(string data)
        {
            ParseData(data);
        }

        public void Update(string data)
        {
            ParseData(data);
        }

        public void Notify(string cmd)
        {
            if (cmd == "print")
            {
                Print();
            }
        }

        public void Print()
        {
            System.Console.WriteLine($"Plane: {Tag} \tAltitude: {Altitude}\t Cords: {XPos},{YPos} \tTs: {TimeStamp}");
        }

        public string Indentify()
        {
            return Tag;
        }
    }
}
