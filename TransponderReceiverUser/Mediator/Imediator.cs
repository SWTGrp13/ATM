using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverUser.Mediator;

namespace TransponderReceiverUser.Mediator
{
    public interface IMediator
    {
        void AddParticipant(IParticipant participant);
        void BroadcastMessage(string message, IParticipant sender);
        void SingleMessage(string message, IParticipant sender);
    }
}
