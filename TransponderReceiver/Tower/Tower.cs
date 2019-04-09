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
using TransponderReceiverLib.Validation;

namespace TransponderReceiverLib.Tower
{
    public class AirTrafficTower
    {
        private static Subject ListOfTracks;
        private FlightLog Log;
        private bool verbose;
        private List<CollisionTracker> collisionTracker_;

        public AirTrafficTower(FlightLog Log, Subject Subject, List<CollisionTracker> tracker, bool verbose = true)
        {
            ListOfTracks = Subject;
            this.Log = Log;
            this.verbose = verbose;
            collisionTracker_ = tracker;
        }

        public void Add(string encodedTransponderMessage)
        {
            var plane = Factory.GetTrack(encodedTransponderMessage);
              
            ListOfTracks.attach(plane);
            
            lock (ListOfTracks)
            {
                // plane/track is in valid airspace.
                if (new Validator().isInValidSpace(plane))
                {
                    ListOfTracks.getInstance(plane.Tag).Update(encodedTransponderMessage);
                }
                // check for hanging data.
                foreach (var currentPlane in ListOfTracks.getInstances().ToList())
                {
                    var p = currentPlane as Track;
                    if (!new Validator().isInValidSpace(p))
                    {
                        ListOfTracks.detach(p);
                    }
                }
            }
        }

        public Subject GetTracks()
        {
            return ListOfTracks;
        }

        public void CollisionValidate()
        {
            var stack = ListOfTracks.getInstances().ToList();
            bool verbose;
            foreach (var _plane in stack)
            {
                verbose = false;
                var plane = _plane as Track;
                var collide = new Calculate().CalculateMetrixes(plane, stack.FindAll(p => p.Identify() != plane.Identify()));
                if (collide.Count == 0)
                {
                    verbose = false;
                    collisionTracker_.RemoveAll(t => t.Tag == plane.Identify());
                }
                else
                {
                    if (!collisionTracker_.Any(t => t.Tag == plane.Identify() && t.CollisionList == string.Join(", ", collide.ToArray())))
                    {
                        verbose = true;
                        collisionTracker_.Add(new CollisionTracker() { Tag = plane.Identify(), CollisionList = string.Join(", ", collide.ToArray()) });
                    }
                }
                Render(collide, plane,verbose);
            }
        }

        public void Render(List<string> status, Track track, bool verbose)
        {
            if (verbose)
            {
                Log.Write(LogLevel.CRITICAL,
                    $"WARNING: {track.Tag} is colliding with {string.Join(", ", status.ToArray())}, {track.TimeStamp}");
            }
           
            Log.Write(LogLevel.NORMAL,
                $"Plane: {track.Tag} \tAltitude: {track.Altitude}\t Cords: {track.XPos},{track.YPos} \tTs: {track.TimeStamp} \t Velocity: {Math.Round(track.Velocity, 2)} m/s \t Degree: {track.Degrees} deg");
        }
    }
}
