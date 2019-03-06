using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverUser.Mediator;
using TransponderReceiverUser.PlaneParticipant;

namespace TransponderReceiverUser.Factory
{
    public static class Factory
    {
        public static void CreatePlane(string receiverData)
        {
            var planeData = receiverData.Split(';');
            Plane frank = new Plane();
            // set plane data...
            Console.WriteLine(receiverData);
        }
    }
}
