using System;
using System.Collections.Generic;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Calculations
{
    public class Calculate : ICalculate
    {
        public double FindDegree(ITrack plane)
        {
            var y = plane.YPos - plane.OldYPos;
            var x = plane.XPos - plane.OldXPos;
            var radians = Math.Atan2(y, x);
            var degree = radians * (180.0 / Math.PI);
            if (degree < 0)
                degree += 360;
            degree = (degree > 0.0 ? degree : 360.0 + degree);
            if (degree == 360)
                return 0.0;
            return Convert.ToInt32(degree);
        }

        public double FindVelocity(ITrack plane)
        {
            var y = Math.Abs(plane.YPos) - Math.Abs(plane.OldYPos);
            var x = Math.Abs(plane.XPos) - Math.Abs(plane.OldXPos);
            var hyp = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var deltaTime = plane.TimeStamp.Subtract(plane.OldTimeStamp).TotalSeconds;
            var velocity = (hyp / deltaTime);
            return Convert.ToInt32(velocity);
        }

       
        public List<string> CalculateMetrixes(ITrack CurrentPlane, List<IObserver> PlanesList)
        {
            
            List<string> result = new List<string>();

            int Xpos = CurrentPlane.XPos;
            int Ypos = CurrentPlane.YPos;
            int[] CurrentPlaneCoordinates = new int[2];
            CurrentPlaneCoordinates[0] = Xpos;
            CurrentPlaneCoordinates[1] = Ypos;

            for (int i = 0; i < PlanesList.Count; i++)
            {
                var plane = PlanesList[i] as Track;
                int[] OtherPlaneCoordinates = { plane.XPos, plane.YPos };
                double xLength = CurrentPlaneCoordinates[0] - OtherPlaneCoordinates[0];
                double yLength = CurrentPlaneCoordinates[1] - OtherPlaneCoordinates[1];
                double HorizontalVector = Math.Sqrt((Math.Pow(xLength, 2)) + (Math.Pow(yLength, 2)));
                double HeightVector = CurrentPlane.Altitude - plane.Altitude;


                if (HorizontalVector <= 5000 && HeightVector <= 300)
                {
                    result.Add(plane.Tag);
                }
            }

            return result;
        }
    }
}
