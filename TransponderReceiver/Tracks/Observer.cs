using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverLib.Tracks
{
    public interface IObserver
    {
        void Update(string data);
        string Identify();
    } 

 
}
