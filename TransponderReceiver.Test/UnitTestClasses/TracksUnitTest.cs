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
            ITrack testTrack= Factory.GetTrack(fakeTrack + time);
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

        [TestCase("XYZ987;25059;75654;4000;")]
        [TestCase("KAT130;45256;8000;14000;")]
        public void TestParseValidSpaceTrue(string fakeTrack)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.isInValidSpace, Is.True);
        }
        [TestCase("XYZ987;85059;75654;4000;")]
        [TestCase("KAT130;45256;85000;14000;")]
        [TestCase("KAT130;45256;8000;40000;")]
        public void TestParseValidSpaceFalse(string fakeTrack)
        {
            ITrack testTrack = Factory.GetTrack(fakeTrack + time);
            Assert.That(testTrack.isInValidSpace, Is.False);
        }

        #endregion

        ////Variables
        //private Track uut;
        //private int OriginalXpos;
        //private int OritignalYpos;
        //private int OriginalAltitude;
        //private string OriginalTimeString;

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

        //// Update function test
        //[Test]
        //public void Update_Test()
        //{
        //    string tid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //    uut.Update("AAA111;50001;50001;10000;" + tid);

        //    //Is true test +1 
        //    Assert.That(uut.OldXPos, Is.EqualTo(OriginalXpos));
        //    Assert.That(uut.OldYPos, Is.EqualTo(OritignalYpos));
        //    Assert.That(uut.Altitude, Is.EqualTo(OriginalAltitude));
        //    Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(OriginalTimeString));

        //    //Sets new variables
        //    uut.Update(("AAA111;49999;49999;10000;" + tid)); //Ved ikke om den her er funktionelt nødvendig, da et begge to bare sætter værdier :P


        //    //Is true -1 
        //    Assert.That(uut.OldXPos, Is.EqualTo(50001));
        //    Assert.That(uut.OldYPos, Is.EqualTo(50001));
        //    Assert.That(uut.Altitude, Is.EqualTo(10000));
        //    Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));


        //    //Sets new variables
        //    uut.Update(("AAA111;50000;50000;10000;" + tid));


        //    //Is true, nothing has changed 
        //    Assert.That(uut.XPos, Is.EqualTo(OriginalXpos));
        //    Assert.That(uut.YPos, Is.EqualTo(OritignalYpos));
        //    Assert.That(uut.Altitude, Is.EqualTo(OriginalAltitude));
        //    Assert.That(uut.TimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));

        //    Assert.That(uut.OldXPos, Is.EqualTo(49999));
        //    Assert.That(uut.OldYPos, Is.EqualTo(49999));
        //    Assert.That(uut.Altitude, Is.EqualTo(10000));
        //    Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));
        //}

        ////Identify test - Den her virker lidt underlig at teste
        //// Men gør det for COVERAGE !
        //[Test]
        //public void Identify_test()
        //{
        //    Assert.That(uut.Tag, Is.EqualTo(uut.Identify()));
        //}

        ////Section for testing set & get metoder
        //[Test]
        //[TestCase(10, 10, true)]
        //public void SetGetMethodsTest(double velocity, double degrees, bool conditionCheck)
        //{
        //    uut.Velocity = velocity;
        //    uut.Degrees = degrees;
        //    uut.ConditionCheck = conditionCheck;

        //    Assert.That(uut.Velocity, Is.EqualTo(velocity));
        //    Assert.That(uut.Degrees, Is.EqualTo(degrees));
        //    Assert.That(uut.ConditionCheck, Is.EqualTo(conditionCheck));


        //}


    }
}