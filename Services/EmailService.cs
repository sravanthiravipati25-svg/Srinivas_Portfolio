using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Portfolio.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> options,
        ILogger<EmailService> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }
    public async Task SendAsync(
    string name,
    string email,
    string message)
    {
        if (string.IsNullOrWhiteSpace(_settings.ApiKey))
        {
            _logger.LogError("Brevo API key is missing.");
            throw new InvalidOperationException(
                "Brevo API key is not configured."
            );
        }

        if (string.IsNullOrWhiteSpace(_settings.From))
        {
            _logger.LogError("Brevo sender email is missing.");
            throw new InvalidOperationException(
                "Brevo sender email is not configured."
            );
        }

        if (string.IsNullOrWhiteSpace(_settings.To))
        {
            _logger.LogError("Brevo recipient email is missing.");
            throw new InvalidOperationException(
                "Brevo recipient email is not configured."
            );
        }

        _logger.LogInformation(
            "Brevo configuration: ApiKeyConfigured={Configured}, From={From}, To={To}",
            !string.IsNullOrWhiteSpace(_settings.ApiKey),
            _settings.From,
            _settings.To
        );

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Add(
            "api-key",
            _settings.ApiKey
        );

        client.DefaultRequestHeaders.Add(
            "accept",
            "application/json"
        );

        var body = new
        {
            sender = new
            {
                name = "Portfolio Website",
                email = _settings.From
            },

            to = new[]
            {
            new
            {
                email = _settings.To
            }
        },

            replyTo = new
            {
                email = email
            },

            subject = $"New Portfolio Contact from {name}",

            htmlContent = $@"
            <html>
            <body>
                <h2>New Portfolio Contact</h2>

                <p>
                    <strong>Name:</strong>
                    {System.Net.WebUtility.HtmlEncode(name)}
                </p>

                <p>
                    <strong>Email:</strong>
                    {System.Net.WebUtility.HtmlEncode(email)}
                </p>

                <hr />

                <p>
                    <strong>Message:</strong>
                </p>

                <p>
                    {System.Net.WebUtility.HtmlEncode(message)
                            .Replace(Environment.NewLine, "<br/>")}
                </p>
            </body>
            </html>"
        };

        _logger.LogInformation(
            "Sending email via Brevo HTTP API..."
        );

        var response = await client.PostAsJsonAsync(
            "https://api.brevo.com/v3/smtp/email",
            body
        );

        var responseText =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                "Brevo API failed: {Status} - {Body}",
                response.StatusCode,
                responseText
            );

            throw new Exception(
                $"Brevo API failed: {responseText}"
            );
        }

        _logger.LogInformation(
            "Email sent successfully via Brevo API."
        );
    }
}