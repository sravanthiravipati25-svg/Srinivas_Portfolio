using System.Text.Json;
using Portfolio.Models;

namespace Portfolio.Services;

/// <summary>
/// Reads Data/portfolio-content.json once and caches the result for the
/// lifetime of the app. Registered as a singleton in Program.cs.
/// </summary>
public class PortfolioContentService : IPortfolioContentService
{
    private readonly Lazy<PortfolioContent> _content;

    public PortfolioContentService(IWebHostEnvironment environment, ILogger<PortfolioContentService> logger)
    {
        _content = new Lazy<PortfolioContent>(() => Load(environment, logger));
    }

    public PortfolioContent GetContent() => _content.Value;

    private static PortfolioContent Load(IWebHostEnvironment environment, ILogger logger)
    {
        var path = Path.Combine(environment.ContentRootPath, "Data", "portfolio-content.json");

        try
        {
            var json = File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = JsonSerializer.Deserialize<PortfolioContent>(json, options);

            if (content is null)
            {
                logger.LogError("Portfolio content file at {Path} deserialized to null.", path);
                return new PortfolioContent();
            }

            return content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load portfolio content from {Path}.", path);
            return new PortfolioContent();
        }
    }
}
