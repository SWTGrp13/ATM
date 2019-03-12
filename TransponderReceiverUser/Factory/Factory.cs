using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public static class Factory
    {
        public static Plane CreatePlane(string transmitterData)
        {
             return new Plane(transmitterData);
        }
    }
}
