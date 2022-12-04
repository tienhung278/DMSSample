namespace Pinewood.DMSSample.Business.Services.Concretes;

public class PartAvailabilityClient : IDisposable, IPartAvailabilityClient
{
    private readonly HttpClient _httpClient;

    public PartAvailabilityClient()
    {
        _httpClient = new HttpClient();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<int> GetAvailability(string stockCode)
    {
        var responseMessage =
            await _httpClient.GetAsync(
                $"https://www.api.pinewood.com/parts/availability/{stockCode}");

        if (!responseMessage.IsSuccessStatusCode) return 500;

        var responseString = await responseMessage.Content.ReadAsStringAsync();

        return int.Parse(responseString);
    }
}