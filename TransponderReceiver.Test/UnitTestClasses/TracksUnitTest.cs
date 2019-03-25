using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class PlaneUnitTest
    {
        //Variables
        private Plane uut;
        private int OriginalXpos;
        private int OritignalYpos;
        private int OriginalAltitude;
        private string OriginalTimeString;


        // SetUp 
        [SetUp]
        public void SetUp()
        {
            OriginalTimeString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            uut = TransponderReceiverLib.Factory.GetPlane("AAA111;50000;50000;10000;"+ OriginalTimeString);
            OriginalXpos = uut.XPos;
            OritignalYpos = uut.YPos;
            OriginalAltitude = uut.Altitude;
        }

        // Update function test
        [Test]
        public void Update_Test()
        {
            string tid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            uut.Update("AAA111;50001;50001;10000;" + tid);

            //Is true test +1 
            Assert.That(uut.OldXPos, Is.EqualTo(OriginalXpos));
            Assert.That(uut.OldYPos, Is.EqualTo(OritignalYpos));
            Assert.That(uut.Altitude, Is.EqualTo(OriginalAltitude));
            Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(OriginalTimeString));

            //Sets new variables
            uut.Update(("AAA111;49999;49999;10000;" + tid)); //Ved ikke om den her er funktionelt nødvendig, da et begge to bare sætter værdier :P


            //Is true -1 
            Assert.That(uut.OldXPos, Is.EqualTo(50001));
            Assert.That(uut.OldYPos, Is.EqualTo(50001));
            Assert.That(uut.Altitude, Is.EqualTo(10000));
            Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));


            //Sets new variables
            uut.Update(("AAA111;50000;50000;10000;" + tid));


            //Is true, nothing has changed 
            Assert.That(uut.XPos, Is.EqualTo(OriginalXpos));
            Assert.That(uut.YPos, Is.EqualTo(OritignalYpos));
            Assert.That(uut.Altitude, Is.EqualTo(OriginalAltitude));
            Assert.That(uut.TimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));

            Assert.That(uut.OldXPos, Is.EqualTo(49999));
            Assert.That(uut.OldYPos, Is.EqualTo(49999));
            Assert.That(uut.Altitude, Is.EqualTo(10000));
            Assert.That(uut.OldTimeStamp.ToString("yyyyMMddHHmmssfff"), Is.EqualTo(tid));
        }

        //Identify test - Den her virker lidt underlig at teste
        // Men gør det for COVERAGE !
        [Test]
        public void Identify_test()
        {
            Assert.That(uut.Tag, Is.EqualTo(uut.Identify()));
        }

        //Section for testing set & get metoder
        [Test]
        [TestCase(10, 10, true)]
        public void SetGetMethodsTest(double velocity, double degrees, bool conditionCheck)
        {
            uut.Velocity = velocity;
            uut.Degrees = degrees;
            uut.ConditionCheck = conditionCheck;

            Assert.That(uut.Velocity, Is.EqualTo(velocity));
            Assert.That(uut.Degrees, Is.EqualTo(degrees));
            Assert.That(uut.ConditionCheck, Is.EqualTo(conditionCheck));


        }


    }
}