using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class TrackUnitTest
    {
        private ITrack _fakeTrack;
        private readonly string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");


        public void Setuo()
        {
            _fakeTrack = Substitute.For<ITrack>();

        }

        #region TestParseData
        [Test]
        public void TestParseDataTag()
        {
            List<String> testTrack = new List<string>();
            ITrack.Track("ATR423;39045;12932;14000;" + time);
            testTrack.Add("BCD123;10005;85890;12000;" + time);
            testTrack.Add("XYZ987;25059;75654;4000;" + time);

            _fakeTrack.
            Assert
        }
        [Test]
        public void TestParseDataXPos()
        {

        }
        [Test]
        public void TestParseDataYPos()
        {

        }
        [Test]
        public void TestParseDataAltitude()
        {

        }
        [Test]
        public void TestParseDataTimeStamp()
        {

        }
        [Test]
        public void TestParseDataVelocity()
        {

        }

        [Test]
        public void TestParseDataDegrees()
        {

        }
        #endregion



        [Test]
        public void TestUpdate()
        {
        }

        [Test]
        public void TestIdentify()
        {
        }

        //[TestCase("ATR423;70045;12932;14000;20151006213456789")] // Time invalid
        //public void TestIsInValidSpace_TimeFalse(string x)
        //{
        //    Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x)), Is.False);
        //}

        //[Test]
        //public void TestNoSeperation()
        //{
        //    var planeOne = Factory.GetPlane("XYZ987;25059;75654;4000;" + Time);
        //    var planeTwo = Factory.GetPlane("ATR423;39045;12932;14000;" + Time);
        //    var planeTree = Factory.GetPlane("BCD123;10005;8890;12000;" + Time);
        //    var listof = new List<IObserver>();
        //    listof.Add(planeTwo);
        //    listof.Add(planeTree);
        //    var result = (Calculate.CalculateMetrixes(planeOne, listof));
        //    int res = result.Count;
        //    Assert.That((res == 0), Is.True);
        //}

    // ^^^^ FRA CALCULATE ^^^^



 
   
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
        //    uut = TransponderReceiverLib.Factory.GetPlane("AAA111;50000;50000;10000;"+ OriginalTimeString);
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


    }
}