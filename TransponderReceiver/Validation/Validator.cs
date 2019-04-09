using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Validation
{
    public class Validator : IValidator
    {
        public bool isInValidSpace(ITrack sub)
        {
            if (((int)sub.XPos < 0) || ((int)sub.XPos > 80000))
            {
                return false;
            }
            if (((int)sub.YPos < 0) || ((int)sub.YPos > 80000))
            {
                return false;
            }
            if (((int)sub.Altitude <= 500) || ((int)sub.Altitude >= 20000))
            {
                return false;
            }
            if ((DateTime.Now.Subtract(sub.TimeStamp).TotalSeconds) >= 2)
            {
                return false;
            }

            return true;
        }
    }
}
