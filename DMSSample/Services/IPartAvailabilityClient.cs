namespace Pinewood.DMSSample.Business.Services;

public interface IPartAvailabilityClient
{
    Task<int> GetAvailability(string stockCode);
}