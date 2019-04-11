using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiverLib.Tracks;
using TransponderReceiverLib;
using TransponderReceiverLib.Calculations;
using TransponderReceiverLib.Validation;
using System.Collections.Generic;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class TrackUnitTest
    {
        private readonly string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        #region TestParseData

        [TestCase("XYZ987;25059;75654;4000;", "XYZ987")]
        [TestCase("KAT130;45256;85000;14000;", "KAT130")]
        public void TestParseTag(string fakeTrack, string expectedTag)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.Tag, Is.EqualTo(expectedTag));
        }

        [TestCase("XYZ987;25059;75654;4000;", 25059)]
        [TestCase("KAT130;45256;85000;14000;", 45256)]
        public void TestParseXPos(string fakeTrack, int expectedXPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.XPos, Is.EqualTo(expectedXPos));
        }

        [TestCase("XYZ987;25059;75654;4000;", 75654)]
        [TestCase("KAT130;45256;85000;14000;", 85000)]
        public void TestParseYPos(string fakeTrack, int expectedYPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.YPos, Is.EqualTo(expectedYPos));
        }

        [TestCase("XYZ987;25059;75654;4000;", 4000)]
        [TestCase("KAT130;45256;85000;14000;", 14000)]
        public void TestParseAltitude(string fakeTrack, int expectedAlt)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.Altitude, Is.EqualTo(expectedAlt));
        }

        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "20161006213456789")]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "20151006213456789")]
        public void TestParseTimeStamp(string fakeTrack, string expectedTime)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack);
            DateTime expectedTimeParsed =
                DateTime.ParseExact(expectedTime, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            Assert.That(testTrack.TimeStamp, Is.EqualTo(expectedTimeParsed));
        }
        #endregion

        [TestCase("XYZ987;25059;75654;4000;", "XYZ987")]
        [TestCase("KAT130;45256;85000;14000;", "KAT130")]
        public void TestIdentify(string fakeTrack, string expectedTag)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.Identify(), Is.EqualTo(expectedTag));
        }

        #region TestUpdate
        //Test OldXPos
        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "XYZ987;26000;80000;4000;20161006213457789", 25059)]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "KAT130;47000;86000;14000;20151006213457789", 45256)]
        public void TestUpdateOldXPos(string fakeTrack, string newTrackData, int expectedOldXPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            testTrack.Update(newTrackData);

            Assert.That(testTrack.OldXPos, Is.EqualTo(expectedOldXPos));
        }

        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "XYZ987;26000;80000;4000;20161006213457789", 26000)]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "KAT130;47000;86000;14000;20151006213457789", 47000)]
        public void TestWrongOldXPos(string fakeTrack, string newTrackData, int newXPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            testTrack.Update(newTrackData);

            Assert.That(testTrack.OldXPos, Is.Not.EqualTo(newXPos));
        }


        //Test OldYPos
        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "XYZ987;26000;80000;4000;20161006213457789", 75654)]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "KAT130;47000;86000;14000;20151006213457789", 85000)]
        public void TestUpdateOldYPos(string fakeTrack, string newTrackData, int expectedOldYPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            testTrack.Update(newTrackData);

            Assert.That(testTrack.OldYPos, Is.EqualTo(expectedOldYPos));
        }

        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "XYZ987;26000;80000;4000;20161006213457789", 80000)]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "KAT130;47000;86000;14000;20151006213457789", 86000)]
        public void TestWrongOldYPos(string fakeTrack, string newTrackData, int newYPos)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            testTrack.Update(newTrackData);

            Assert.That(testTrack.OldXPos, Is.Not.EqualTo(newYPos));
        }

        [TestCase("XYZ987;25059;75654;4000;20161006213456789", "XYZ987;26000;80000;4000;20161006213457789", "20161006213456789")]
        [TestCase("KAT130;45256;85000;14000;20151006213456789", "KAT130;47000;86000;14000;20191006213457789", "20151006213456789")]
        public void TestUpdateOldTime(string fakeTrack, string newTrackData, string oldTimeStampString)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack);
            testTrack.Update(newTrackData);
            DateTime expectedTimeStamp =
                DateTime.ParseExact(oldTimeStampString, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            Assert.That(testTrack.OldTimeStamp, Is.EqualTo(expectedTimeStamp));
        }
        #endregion


        //// SetUp 
        //[SetUp]
        //public void SetUp()
        //{
        //    OriginalTimeString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //    uut = TransponderReceiverLib.Factory.GetTrack("AAA111;50000;50000;10000;"+ OriginalTimeString);
        //    OriginalXpos = uut.XPos;
        //    OritignalYpos = uut.YPos;
        //    OriginalAltitude = uut.Altitude;
        //}



    }
}