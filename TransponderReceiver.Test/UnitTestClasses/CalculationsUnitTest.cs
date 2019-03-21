using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TransponderReceiverLib;
using TransponderReceiverLib.Tracks;
using TransponderReceiverLib.Calculations;


namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    public class CalculationsUnitTest
    {
        private readonly string Time = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        [TestCase("ATR423;39045;12932;14000;")]
        [TestCase("BCD123;10005;8890;12000;")]
        [TestCase("XYZ987;25059;75654;4000;")]
        public void TestisInValidSpace_True(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x+Time)), Is.True);
        }


        [TestCase("ATR423;90045;12932;14000;")] // XPos Invalid
        [TestCase("BCD123;10005;90890;12000;")] // YPos Invalid
        [TestCase("XYZ987;25059;75654;400;")] // Altitude Invalid
        [TestCase("KAT130;90045;12932;21000;")] // Altitude Invalid
        public void TestisInvalidSpace_PosFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x+Time)), Is.False);
        }

        [TestCase("ATR423;90045;12932;14000;20151006213456789")] // Time invalid
        public void TestIsInValidSpace_TimeFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x)), Is.False);
        }


        //[Test]
        //public void TestAirTrafficTowerExpectsInstanceAlike()
        //{
        //    FileConfig cfg = Factory.GetFileCofig("a", "b");
        //    FlightLog log = Factory.GetFlightLog(cfg);
        //    Subject ObserverList = Factory.GetSubject();
        //    Assert.IsInstanceOf<AirTrafficTower>(Factory.GetTower(log, ObserverList));
        //}

        //private Calculate UUT;

        //[SetUp]
        //public void SetUp()
        //{
        //    UUT = new Calculate();
        //}

        //[TestCase()]
        //public void FindDegree_TRUE(int x, int y, int degree)
        //{
        //    Assert.That(UUT.FrindDegree(x, y), Is.EqualTo(degree).Within(0.01));
        //}

        //[TestCase()]
        //public void FindDegree_FALSE(int x, int y, int degree)
        //{
        //    Assert.That(UUT.FrindDegree(x, y), Is.Not.EqualTo(degree).Within(0.01));
        //}

        //[TestCase()]
        //public void FindVelocity_TRUE(int x, int xOld, int y, int yOld, int Velocity)
        //{
        //    Assert.That(UUT.FindVelocity(x, xOld, y, yOld), Is.EqualTo(Velocity));
        //}

    }
}
