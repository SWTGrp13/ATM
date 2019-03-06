using System;
using TransponderReceiverUser.Mediator;

namespace TransponderReceiverUser.PlaneParticipant
{
    public class Plane : IPlane
    {
        private IMediator _Mediator = null;

        string IPlane.Tag { get; set; }
        int IPlane.XPos { get; set; }
        int IPlane.YPos { get; set; }
        int IPlane.Altitude { get; set; }
        DateTime IPlane.TimeStamp { get; set; }

        public void SetMoitor(IMediator mediator)
        {
            _Mediator = mediator;
        }

        void IParticipant.SendMessage(string message)
        {
            _Mediator?.SingleMessage(message, this);
        }
    }
  
}
