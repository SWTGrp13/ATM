using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.ObserverPattern
{
    public interface IObserver
    {
        void Update(string tag);
        string Indentify();
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
