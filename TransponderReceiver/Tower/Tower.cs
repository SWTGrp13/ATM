using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverLib.Log;
using TransponderReceiverLib.Calculations;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Tower
{
    public class AirTrafficTower
    {
        private static Subject ListOfTracks;
        private FlightLog Log;
        private bool verbose;

        public AirTrafficTower(FlightLog Log, Subject Subject, bool verbose = true)
        {
            ListOfTracks = Subject;
            this.Log = Log;
            this.verbose = verbose;
        }

        public void Add(string encodedTransponderMessage)
        {
            var plane = Factory.GetPlane(encodedTransponderMessage);
              
            ListOfTracks.attach(plane);
            
            lock (ListOfTracks)
            {
                // plane/track is in valid airspace.
                if (Calculate.isInValidSpace(plane))
                {
                    ListOfTracks.getInstance(plane.Tag).Update(encodedTransponderMessage);
                }
                // check for hanging data.
                //.FindAll(a => a.Identify() != plane.Tag)
                foreach (var currentPlane in ListOfTracks.getInstances().ToList())
                {
                    var p = currentPlane as Plane;
                    if (!Calculate.isInValidSpace(p))
                    {
                        ListOfTracks.detach(p);
                    }
                }
            }
        }

        public void Render()
        {
            var stack = ListOfTracks.getInstances().ToList();
            foreach (var _plane in stack)
            {
                var plane = _plane as Plane;
                List<string> collide = Calculate.CalculateMetrixes(plane, stack.FindAll(p => p.Identify() != plane.Tag));

                if (collide.Count > 0 && plane.ConditionCheck == false)
                {
                    plane.ConditionCheck = true;
                    Log.Write(LogLevel.CRITICAL, $"WARNING: {plane.Tag} is colliding with { String.Join(", ", collide.ToArray()) }, { plane.TimeStamp }");
                }
                else
                {
                    plane.ConditionCheck = false;
                    if (verbose)
                    {
                        Log.Write(LogLevel.NORMAL, $"Plane: {plane.Tag} \tAltitude: {plane.Altitude}\t Cords: {plane.XPos},{plane.YPos} \tTs: {plane.TimeStamp} - {DateTime.Now.Subtract(plane.TimeStamp).TotalSeconds} \t Velocity: {Math.Round(plane.Velocity, 2)} m/s \t Degree: {plane.Degrees} deg");
                    }
                }
            }
        }
    }
}
