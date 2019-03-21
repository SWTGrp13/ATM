using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverLib.Log
{
   
    public enum LogLevel
    {
        NORMAL,
        WARNING,
        CRITICAL
    }
    // template pattern
    public abstract class Log
    {
        public abstract void Write(LogLevel level, string message);
    }
}
