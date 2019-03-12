using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.ObserverPattern
{
    public interface IObserver
    {
<<<<<<< HEAD
        void Update(string tag);
        string Indentify();
=======
        void Update(Plane p);
>>>>>>> 2737a3c092080d2d86a83d5db9720479ddb02d53
    }

    public class Observer : IObserver
    {
        public void Update(Plane p)
        {
                System.Console.WriteLine("{data}");     
        }

        public string Indentify()
        {
            throw new NotImplementedException();
        }
    }

}
