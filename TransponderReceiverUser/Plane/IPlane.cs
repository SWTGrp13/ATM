using System;
using TransponderReceiverUser.ObserverPattern;

namespace TransponderReceiverUser
{
    public interface IPlane : IObserver
    {
        string Tag { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }
        int Altitude { get; set; }
        int Velocity { get; set; }
        int Degrees { get; set; }
        bool ConditionCheck { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
