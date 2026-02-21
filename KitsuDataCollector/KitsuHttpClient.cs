using Serilog;

namespace KitsuDataCollector;

public class KitsuHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public KitsuHttpClient(HttpClient httpClient, ILogger logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://kitsu.io/api/edge/");
        _logger = logger;
    }

    public async Task<string> GetAnimeByIdAsync(int id, CancellationToken cancellationToken)
    {
        try {
            using var responce = await _httpClient.GetAsync($"/anime/{id}", cancellationToken);
            responce.EnsureSuccessStatusCode();
            return await responce.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while processing the request.");
            throw;
        }
    }
}
