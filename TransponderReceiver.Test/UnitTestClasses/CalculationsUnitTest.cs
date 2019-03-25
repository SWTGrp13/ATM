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

     // Test of isInValidSpace

        private readonly string Time = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        [TestCase("ATR423;39045;12932;14000;")]
        [TestCase("BCD123;10005;8890;12000;")]
        [TestCase("XYZ987;70000;9000;19999;")]
        [TestCase("MIW248;2549;75654;501;")]
        public void TestisInValidSpace_True(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x + Time)), Is.True);
        }

        [TestCase("ATR423;90045;12932;14000;")] // XPos Invalid
        [TestCase("ATR423;-55;12932;14000;")] // XPos Invalid
        [TestCase("BCD123;10005;90890;12000;")] // YPos Invalid
        [TestCase("BCD123;10005;-890;12000;")] // YPos Invalid
        [TestCase("XYZ987;25059;75654;400;")] // Altitude Invalid
        [TestCase("KAT130;9005;12932;21000;")] // Altitude Invalid
        [TestCase("KAT130;90045;90890;21000;")] // All Invalid
        public void TestisInvalidSpace_PosFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x + Time)), Is.False);
        }

        [TestCase("KAT130;90045;12932;21000;", 5)] // DateTime Invalid
        [TestCase("KAT130;90045;12932;21000;", 3)] // DateTime Invalid
        public void TestTimeIsExceeded(string eventdata, int diff)
        {
            var TimeSpan = new DateTime().Add(new TimeSpan(diff, 0, 0)).ToString("yyyyMMddHHmmssfff");
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(eventdata + TimeSpan)), Is.False);
        }


        [TestCase("XYZ987;25059;75654;4000;",0)]
        public void TestTimeIsGood(string eventdata, int diff)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(eventdata + Time)), Is.True);
        }

        [TestCase("ATR423;90045;12932;14000;20151006213456789")] // Time invalid
        [TestCase("ATR423;90045;12932;14000;20201006213456789")] // Time invalid
        [TestCase("ATR423;90045;12932;14000;20161006213456789")] // Time invalid
        public void TestIsInValidSpace_TimeFalse(string x)
        {
            Assert.That(Calculate.isInValidSpace(Factory.GetPlane(x)), Is.False);
        }

        [Test]
        public void TestNoSeperation()
        {
            var planeOne = Factory.GetPlane("XYZ987;25059;75654;4000;" + Time);
            var planeTwo = Factory.GetPlane("ATR423;39045;12932;14000;" + Time);
            var planeTree = Factory.GetPlane("BCD123;10005;8890;12000;" + Time);
            var listof = new List<IObserver>();
            listof.Add(planeTwo);
            listof.Add(planeTree);
            var result = (Calculate.CalculateMetrixes(planeOne, listof));
            int res = result.Count;
            Assert.That((res == 0),Is.True);
        }

        [Test]
        public void TestIsSeperations()
        {
            var planeOne = Factory.GetPlane("XYZ987;25059;75654;4000;" + Time);
            var planeTwo = Factory.GetPlane("ATR423;25059;75654;14000;" + Time);
            var planeTree = Factory.GetPlane("BCD123;25059;8890;12000;" + Time);
            var listof = new List<IObserver>();
            listof.Add(planeTwo);
            listof.Add(planeTree);
            var result = (Calculate.CalculateMetrixes(planeOne, listof));
            int res = result.Count;
            Assert.That((res > 0), Is.True);
        }


        //Test of FindDregee

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
