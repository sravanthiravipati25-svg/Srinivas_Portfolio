namespace Portfolio.Models;

public class HomeViewModel
{
    public PortfolioContent Content { get; set; } = new();
    public ContactFormModel ContactForm { get; set; } = new();
}
