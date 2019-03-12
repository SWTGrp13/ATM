using System;


namespace Subject
{

public interface Isubject
{
	void attach(Observer NyeObserver);
	void detach(Observer FjernetObserver);
	void notify();
}

public class Subject : Isubject
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

}}