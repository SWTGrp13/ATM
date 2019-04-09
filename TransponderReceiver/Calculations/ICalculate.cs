using System.Collections.Generic;
using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Calculations
{
    public interface ICalculate
    {
        double FindDegree(ITrack plane);
        double FindVelocity(ITrack plane);
        List<string> CalculateMetrixes(ITrack CurrentPlane, List<IObserver> PlanesList);
    }
}