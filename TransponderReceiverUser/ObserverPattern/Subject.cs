using System;
using System.Collections.Generic;


namespace TransponderReceiverUser.ObserverPattern
{

    public interface ISubject
    {
	    void attach(Observer NyeObserver);
	    void detach(Observer FjernetObserver);
	    void notify();
    }

    public class Subject : ISubject
    {
	    private List<Observer> Observerlist; //Liste over tilføjet observers af subject'et
	    
	    public void Attach(Observer NyeObserver) //Tilføjer en observer til observerlist 
	    {
		    Observerlist.Add(NyeObserver);
	    }
	    
	    public void Detach(Observer FjernetObserver) // Kigger gennem observerliste og fjerner observeren hvis den er i listen
	    {
		    if(Observerlist.Contains(FjernetObserver) == true)
		    {
			    Observerlist.Remove(FjernetObserver);
		    }

	    }

	    public void Notify()
	    {
		    Observerlist.ForEach(i => i.Update());
	    }

    }

}