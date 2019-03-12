using System;
using System.Collections.Generic;


namespace TransponderReceiverUser.ObserverPattern
{

    public interface ISubject
    {
	    void attach(Observer NyeObserver);
	    void detach(Observer FjernetObserver);
	    void notify(string data);
    }

    public class Subject : ISubject
    {
	    private List<Observer> Observerlist; //Liste over tilføjet observers af subject'et
	    
        public void attach(Observer NyeObserver)
        {
            Observerlist.Add(NyeObserver);
        }

        public void detach(Observer FjernetObserver)
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