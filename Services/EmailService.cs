using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Portfolio.Models;

namespace Portfolio.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(
            HttpClient httpClient,
            IOptions<EmailSettings> options,
            ILogger<EmailService> logger)
        {
            _httpClient = httpClient;
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
                _logger.LogError(
                    "Brevo API key is missing.");

                throw new InvalidOperationException(
                    "Brevo API key is not configured.");
            }

            if (string.IsNullOrWhiteSpace(_settings.From))
            {
                _logger.LogError(
                    "Brevo sender email is missing.");

                throw new InvalidOperationException(
                    "Brevo sender email is not configured.");
            }

            if (string.IsNullOrWhiteSpace(_settings.To))
            {
                _logger.LogError(
                    "Brevo recipient email is missing.");

                throw new InvalidOperationException(
                    "Brevo recipient email is not configured.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Name is required.",
                    nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(
                    "Email is required.",
                    nameof(email));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(
                    "Message is required.",
                    nameof(message));
            }

            _logger.LogInformation(
                "Brevo configuration: ApiKeyConfigured={Configured}, From={From}, To={To}",
                !string.IsNullOrWhiteSpace(_settings.ApiKey),
                _settings.From,
                _settings.To);

            var emailBody = new
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
                    email = email,
                    name = name
                },

                subject = $"New Portfolio Contact from {name}",

                htmlContent = $"""
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset="UTF-8">
                        <title>New Portfolio Contact</title>
                    </head>

                    <body style="font-family: Arial, sans-serif; line-height: 1.6;">

                        <h2>New Portfolio Contact</h2>

                        <p>
                            <strong>Name:</strong>
                            {WebUtility.HtmlEncode(name)}
                        </p>

                        <p>
                            <strong>Email:</strong>
                            {WebUtility.HtmlEncode(email)}
                        </p>

                        <hr />

                        <p>
                            <strong>Message:</strong>
                        </p>

                        <p>
                            {WebUtility.HtmlEncode(message)
                                .Replace("\r\n", "<br>")
                                .Replace("\n", "<br>")}
                        </p>

                    </body>
                    </html>
                    """
            };

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.brevo.com/v3/smtp/email");

            request.Headers.Add(
                "api-key",
                _settings.ApiKey);

            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));

            request.Content = JsonContent.Create(emailBody);

            _logger.LogInformation(
                "Sending email via Brevo HTTP API...");

            using var response =
                await _httpClient.SendAsync(request);

            var responseText =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "Brevo API failed. StatusCode={StatusCode}, Response={Response}",
                    (int)response.StatusCode,
                    responseText);

                throw new InvalidOperationException(
                    $"Brevo API failed: {responseText}");
            }

            _logger.LogInformation(
                "Email sent successfully via Brevo API. Response={Response}",
                responseText);
        }
    }
}