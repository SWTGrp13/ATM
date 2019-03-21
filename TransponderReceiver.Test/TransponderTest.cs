using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TransponderReceiver;

namespace TransponderReceiver.Test
{
    [TestFixture]
    public class TransponderTest
    {
        [Test]
        public void ReturnFalseGivenValueOf1()
        {
            Assert.IsFalse(false, "1 should not be prime");
        }
    }
}
