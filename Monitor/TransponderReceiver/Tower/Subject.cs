using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Tower
{
    public interface ISubject
    {
        bool attach(IObserver NyeObserver);
        void detach(IObserver FjernetObserver);
        IObserver getInstance(string tag);
    }

    public class Subject : ISubject
    {
        private List<IObserver> Observerlist = new List<IObserver>();

        public bool attach(IObserver NyeObserver)
        {
        
                bool plane = false;
                
                string obs = NyeObserver.Identify();
                plane = Observerlist.Any(a => a.Identify() == obs);
                if (plane == false)
                {
                    Observerlist.Add(NyeObserver);
                    return true;
                }

            return false;
        }

        public void detach(IObserver FjernetObserver)
        {
            lock (Observerlist)
                Observerlist.RemoveAll(a => a.Identify() == FjernetObserver.Identify());
        }

        public IObserver getInstance(string tag)
        {
            return Observerlist.Single(a => a.Identify() == tag);
        }

        public List<IObserver> getInstances()
        {
            return Observerlist;
        }

    }
}
