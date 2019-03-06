using System;
using TransponderReceiverUser.Mediator;

namespace TransponderReceiverUser.PlaneParticipant
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
