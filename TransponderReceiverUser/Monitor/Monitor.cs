using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverUser.Mediator;

namespace TransponderReceiverUser
{
    public class Monitor : IMediator
    {
        private List<IParticipant> _Participants = new List<IParticipant>();

        public void AddParticipant(IParticipant participant)
        {
            _Participants.Add(participant);
        }

        public void BroadcastMessage(string message, IParticipant sender)
        {
            throw new NotImplementedException();
        }

        public void SingleMessage(string message, IParticipant sender)
        {
           Console.WriteLine("Message from plane" + message);
        }
    }
}
