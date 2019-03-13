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
        double Velocity { get; set; }
        double Degrees { get; set; }
        bool ConditionCheck { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
