using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TransponderReceiverLib;
using TransponderReceiverLib.Calculations;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class CalculationsUnitTest
    {
        private IPlane _FakePlane;


        //[Test]

        //public static bool isInvalidSpace(Plane sub)
        //{
        //    if (((int)sub.XPos < 0) || ((int)sub.XPos > 80000))
        //    {
        //        return false;
        //    }
        //    if (((int)sub.YPos < 0) || ((int)sub.YPos > 80000))
        //    {
        //        return false;
        //    }
        //    if (((int)sub.Altitude <= 500) || ((int)sub.YPos >= 20000))
        //    {
        //        return false;
        //    }
        //    if ((DateTime.Now.Subtract(sub.TimeStamp).TotalSeconds) >= 2)
        //    {
        //        return false;
        //    }

        //    return true;
        //}



    }
}

//public class TestTransponderReceiverClient
//{
//    private ITransponderReceiver _fakeTransponderReceiver;
//    private TransponderReceiverClient _uut;
//    [SetUp]
//    public void Setup()
//    {
//        // Make a fake Transponder Data Receiver
//        _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
//        // Inject the fake TDR
//        _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
//    }

//    [Test]
//    public void TestReception()
//    {
//        // Setup test data
//        List<string> testData = new List<string>();
//        testData.Add("ATR423;39045;12932;14000;20151006213456789");
//        testData.Add("BCD123;10005;85890;12000;20151006213456789");
//        testData.Add("XYZ987;25059;75654;4000;20151006213456789");

//        // Act: Trigger the fake object to execute event invocation
//        _fakeTransponderReceiver.TransponderDataReady
//            += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

//        // Assert something here or use an NSubstitute Received
//    }
//}
