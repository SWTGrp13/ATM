using System;
using System.Collections.Generic;
using System.Data;
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
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x + Time)), Is.True);
        }


        [TestCase("ATR423;90045;12932;14000;")] // XPos Invalid
        [TestCase("ATR423;-55;12932;14000;")] // XPos Invalid
        [TestCase("BCD123;10005;90890;12000;")] // YPos Invalid
        [TestCase("BCD123;10005;-890;12000;")] // YPos Invalid
        [TestCase("XYZ987;25059;75654;400;")] // Altitude Invalid
        [TestCase("KAT130;90045;12932;21000;")] // Altitude Invalid
        public void TestisInvalidSpace_PosFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x + Time)), Is.False);
        }

        [TestCase("ATR423;90045;12932;14000;20151006213456789")] // Time invalid
        [TestCase("ATR423;90045;12932;14000;20201006213456789")] // Time invalid
        [TestCase("ATR423;90045;12932;14000;20161006213456789")] // Time invalid
        public void TestIsInValidSpace_TimeFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x)), Is.False);
        }

        [TestCase("KAT130;0;0;21000;20161006213456789", "KAT130;0;0;21000;20161006213456789", 0)]
        [TestCase("KAT130;6436;33560;21000;20161006213456789", "KAT130;6538;33540;21000;20161006213456789", 349)]
        [TestCase("KAT130;5356;47056;21000;20161006213456789", "KAT130;5415;47071;21000;20161006213456789", 14)]
        public void TestFindDegree(string oldData, string newData, int degree)
        {
            Plane testPlane = new Plane(oldData);
            testPlane.Update(newData);

            Assert.That(Calculate.FindDegree(testPlane), Is.EqualTo(degree));
        }

    }
}
