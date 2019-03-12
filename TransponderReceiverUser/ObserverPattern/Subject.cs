using System;
using System.Collections.Generic;
using System.Linq;


namespace TransponderReceiverUser.ObserverPattern
{

    public interface ISubject
    {
	    bool attach(IObserver NyeObserver);
	    void detach(IObserver FjernetObserver);
        IObserver getInstance(string tag);
        void notify(string data);
    }

    public class Subject : ISubject
    {
	    private List<IObserver> Observerlist = new List<IObserver>();
	    
        public bool attach(IObserver NyeObserver)
        {
            List<IObserver> member = Observerlist.FindAll(a => a.Indentify() == NyeObserver.Indentify());
            if (member.Count == 0)
            {
                Observerlist.Add(NyeObserver);
                return true;
            }

            return false;
        }

        public void detach(IObserver FjernetObserver)
        {
            if(Observerlist.Contains(FjernetObserver))
            {
                Observerlist.Remove(FjernetObserver);
            }
        }

        public void notify(string data)
        {
            Observerlist.ForEach(i => i.Notify(data));
        }

        public IObserver getInstance(string tag)
        {
            return Observerlist.Single(a => a.Indentify() == tag);
        }
    }
}