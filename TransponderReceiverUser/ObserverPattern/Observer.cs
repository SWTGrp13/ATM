using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.ObserverPattern
{
    public interface IObserver
    {
        void Update(Plane p);
    }

    public class Observer : IObserver
    {
        public void Update(Plane p)
        {
                System.Console.WriteLine("{data}");     
        }
    }
}
