using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Controllers;

public class HomeController : Controller
{
    private readonly IEmailService _emailService;
    private readonly IPortfolioContentService _contentService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IEmailService emailService,
        IPortfolioContentService contentService,
        ILogger<HomeController> logger)
    {
        _emailService = emailService;
        _contentService = contentService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            Content = _contentService.GetContent(),
            ContactForm = new ContactFormModel()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactFormModel contactForm)
    {
        if (!ModelState.IsValid)
        {
            var model = new HomeViewModel
            {
                Content = _contentService.GetContent(),
                ContactForm = contactForm
            };

            TempData["Error"] = "Please fix the highlighted fields and try again.";

            return View("Index", model);
        }

        try
        {
            _logger.LogInformation("Contact form submitted from {Email}", contactForm.Email);

            await _emailService.SendAsync(
                contactForm.Name,
                contactForm.Email,
                contactForm.Message);

            TempData["Success"] = "Thank you! Your message has been sent successfully. I'll get back to you soon.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Contact form email failed");

            var person = _contentService.GetContent().Person;

            TempData["Error"] =
                $"Sorry, the message could not be sent right now. Please email me directly at {person.Email}";

            return RedirectToAction(nameof(Index));
        }
    }

    public IActionResult Error()
    {
        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };

        return View(model);
    }
}
