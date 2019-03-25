using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;
using TransponderReceiverLib;
using TransponderReceiverLib.Log;
using TransponderReceiverLib.Tower;

namespace TransponderReceiver.Test.UnitTestClasses
{
    class TowerUnitTest
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private static AirTrafficTower _uut;
        private static FileConfig cfg;
        private static FlightLog Log;
        private static Subject TrackList;

        [SetUp]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();

            cfg = Factory.GetFileCofig($"UUT_GRP_13_TEST_2.txt", System.IO.Path.GetTempPath());

            Log = Factory.GetFlightLog(cfg);

            TrackList = Factory.GetSubject();

            _uut = new AirTrafficTower(Log, TrackList);
        }
        
        static void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                _uut.Add(data);
            }
        }

        [Test]
        public void TestDataInserted()
        {
            String time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;"+ time);
            testData.Add("BCD123;10005;85890;12000;"+ time);
            testData.Add("XYZ987;25059;75654;4000;"+ time);

            // assign dummy event
            _fakeTransponderReceiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
                
            Assert.AreEqual(2, _uut.GetTracks().getInstances().Count);

        }

        [Test]
        public void TestDataInsertedAndRender()
        {
            String time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;" + time);
            testData.Add("BCD123;10005;85890;12000;" + time);
            testData.Add("XYZ987;25059;75654;4000;" + time);

            // assign dummy event
            _fakeTransponderReceiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            _uut.Render();

            Assert.AreEqual(2, _uut.GetTracks().getInstances().Count);

        }


        [Test]
        public void TestDataInsertedAndRenderCollision()
        {
            String time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;" + time);
            testData.Add("BCD123;39045;85890;12000;" + time);
            testData.Add("XYZ987;39045;75654;4000;" + time);

            // assign dummy event
            _fakeTransponderReceiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            _uut.Render();

            Assert.AreEqual(2, _uut.GetTracks().getInstances().Count);

        }
        [Test]
        public void TestDataInsertedAndObserverExsists()
        {
            String time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;" + time);
            testData.Add("ATR423;39045;85890;12000;" + time);
            testData.Add("XYZ987;39045;75654;4000;" + time);

            // assign dummy event
            _fakeTransponderReceiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            _uut.Render();

            Assert.AreEqual(2, _uut.GetTracks().getInstances().Count);

        }


        [Test]
        public void TestDataInsertedAndCollisionIsFlagged()
        {
            String time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            // Setup test data
 
            List<string> testData = new List<string>();
            testData.Add("XYZ987;25059;75654;4000;" + time);
            testData.Add("ATR423;25059;75654;14000;" + time);
            testData.Add("BCD123;25059;8890;12000;" + time);

            // assign dummy event
            _fakeTransponderReceiver.TransponderDataReady += ReceiverOnTransponderDataReady;

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));
            _uut.Render();

            Assert.AreEqual(3, _uut.GetTracks().getInstances().Count);

        }

    }
}
