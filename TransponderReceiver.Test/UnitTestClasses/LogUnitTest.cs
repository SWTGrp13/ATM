using System;
using System.Collections.Generic;
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

        private FlightLog log;
        [SetUp]
        public void SetUp()
        {
            FileConfig cfg = new FileConfig("testFixture.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            log = new FlightLog(cfg);
        }
    
        [Test]
        [TestCase(LogLevel.NORMAL,"NORMAL")]
        [TestCase(LogLevel.WARNING, "WARNING")]
        [TestCase(LogLevel.CRITICAL, "TING_FLYVER_IND_I_TING")]
        public void TestLogConfig(LogLevel level, string message)
        {
            log.Write(level, message);   
        }

        [TestCase(LogLevel.WARNING,ConsoleColor.DarkYellow)]
        [TestCase(LogLevel.NORMAL,ConsoleColor.White)]
        [TestCase(LogLevel.CRITICAL, ConsoleColor.Red)]
        public void TestLogBistandsPensel(LogLevel level, ConsoleColor ExpectedColor)
        {
            //  there is no way to test for current console color source: N-Unit forum. tryed Console.ForegroundColor ExpectedColor
            try
            {       
                log.FormatConsole(level);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(true);
            }
        }
    }
}
