﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TransponderReceiverLib;
using TransponderReceiverLib.Log;
using TransponderReceiverLib.Tower;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class FactoryUnitTest
    {
        [Test]
        public void uutTestGetTrackerListAreInstanceOf()
        {
            List<CollisionTracker> tracker = Factory.GetTracker();
            Assert.IsInstanceOf<List<CollisionTracker>>(tracker);
        }

        [Test]
        public void TestFileConfigExpectsInstanceAlike()
        {
            FileConfig fromFactory = Factory.GetFileConfig("a", "b");
            Assert.IsInstanceOf<FileConfig>(fromFactory);
        }
        [Test]
        public void TestFlightLogExpectsInstanceAlike()
        {
            FileConfig cfg = Factory.GetFileConfig("a", "b");
            FlightLog log = Factory.GetFlightLog(cfg);
            Assert.IsInstanceOf<FlightLog>(log);
        }
        [Test]
        public void TestPlaneExpectsInstanceAlike()
        {
            Assert.IsInstanceOf<ITrack>(Factory.GetTrack("XYZ987;25059;75654;4000;20151006213456789"));
        }
        [Test]
        public void TestSubjectExpectsInstanceAlike()
        {
            Assert.IsInstanceOf<Subject>(Factory.GetSubject());
        }
        [Test]
        public void TestAirTrafficTowerExpectsInstanceAlike()
        {
            FileConfig cfg = Factory.GetFileConfig("a", "b");
            FlightLog log = Factory.GetFlightLog(cfg);
            Subject ObserverList = Factory.GetSubject();
            List<CollisionTracker> tracker = new List<CollisionTracker>();
            Assert.IsInstanceOf<AirTrafficTower>(Factory.GetTower(log,ObserverList, tracker));
        }
    }
}
