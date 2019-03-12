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
    }

    public class Observer : IObserver
    {
        public void Update(string tag)
        {
            foreach (var data in tag)
            {
                System.Console.WriteLine("{data}");

            }
            
        }
    }
}
