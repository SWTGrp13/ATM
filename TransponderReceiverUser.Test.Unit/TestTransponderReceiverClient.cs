﻿using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;
using TransponderReceiverUser.Calculations;

namespace TransponderReceiverUser.Test.Unit
{
    public class TestTransponderReceiverClient
    {
        private ITransponderReceiver _fakeTransponderReceiver;
        private TransponderReceiverClient _uut;
        [SetUp]
        public void Setup()
        {
            // Make a fake Transponder Data Receiver
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            // Inject the fake TDR
            _uut = new TransponderReceiverClient(_fakeTransponderReceiver);
        }

        [Test]
        public void TestReception()
        {
            // Setup test data
            List<string> testData = new List<string>();
            testData.Add("ATR423;39045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85890;12000;20151006213456789");
            testData.Add("XYZ987;25059;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            // Assert something here or use an NSubstitute Received
        }
    }

    public class TestCalculations
    {
        private Calculate _uut;

        [SetUp]

        public void Setup()
        {
            _uut = new Calculate();
        }

        [TestCase(3, 3, 6, 6, 45)]
        [TestCase(6, 6, 3, 3, 225)]
        [TestCase(3, 3, 3, 4, 0)] //North
        public void TestCourseCalculation(double x1, double y1, double x2, double y2, double z)
        {
            Assert.That(_uut.FindAngle(x1, y1, x2, y2), Is.EqualTo(z));
        }
    }


}




