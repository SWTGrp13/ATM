using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverUser.Mediator;

namespace TransponderReceiverUser.Plane
{
    public interface IPlane : IParticipant
    {
        string Tag { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }
        int Altitude { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
