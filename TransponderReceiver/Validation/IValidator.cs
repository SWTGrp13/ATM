using TransponderReceiverLib.Tracks;

namespace TransponderReceiverLib.Validation
{
    public interface IValidator
    {
        bool isInValidSpace(ITrack sub);
    }
}