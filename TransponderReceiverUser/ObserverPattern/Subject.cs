using System;
using System.Collections.Generic;
using System.Linq;


namespace TransponderReceiverUser.ObserverPattern
{

    public interface ISubject
    {
	    void attach(IObserver NyeObserver);
	    void detach(IObserver FjernetObserver);
	    void notify(string data);
    }

    public class Subject : ISubject
    {
	    private List<IObserver> Observerlist = new List<IObserver>();
	    
        public void attach(IObserver NyeObserver)
        {
            List<IObserver> member = Observerlist.FindAll(a => a.Indentify() == NyeObserver.Indentify());
            if (member.Count == 0)
            {
                Observerlist.Add(NyeObserver);
            }
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
            Observerlist.ForEach(i => i.Update(data));
        }
    }
}