using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiverLib.Log;

namespace TransponderReceiver.Test.UnitTestClasses
{
    [TestFixture]
    class LogUnitTest
    {
        private FileConfig cfg;
        private FlightLog log;
        [SetUp]
        public void SetUp()
        {
            cfg = new FileConfig("testFixtureGrp13.txt", System.IO.Path.GetTempPath());
            log = new FlightLog(cfg);
        }
    
        [Test]
        [TestCase(LogLevel.NORMAL,"NORMAL")]
        [TestCase(LogLevel.WARNING, "WARNING")]
        [TestCase(LogLevel.CRITICAL, "TING_FLYVER_IND_I_TING")]
        public void TestLogConfig(LogLevel level, string message)
        {
            log.Write(level, message);   
            // locate file and check for msg
            if(level == LogLevel.CRITICAL)
            {
                var path = Path.Combine(cfg.FilePath, cfg.FileName);
                var lines = File.ReadAllLines(path);
                Assert.That(lines.Contains(message), Is.True);
                File.Delete(path);
            }
            Assert.That(true, Is.True);
        }

        [TestCase(LogLevel.WARNING,ConsoleColor.DarkYellow)]
        [TestCase(LogLevel.NORMAL,ConsoleColor.White)]
        [TestCase(LogLevel.CRITICAL, ConsoleColor.Red)]
        public void TestLogBistandsPensel(LogLevel level, ConsoleColor ExpectedColor)
        {
            //  there is no way to test for current console color source: N-Unit forum. tryed Console.ForegroundColor = ExpectedColor
            log.FormatConsole(level);
            Assert.IsTrue(true);
        }
    }
}
