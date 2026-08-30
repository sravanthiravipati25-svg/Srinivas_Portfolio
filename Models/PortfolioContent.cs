namespace Portfolio.Models;

// Strongly-typed representation of Data/portfolio-content.json.
// Keeping all page copy in that JSON file (instead of hardcoded in the
// Razor view) means the site's content can be updated without touching
// any markup, CSS or C# code.

public class PortfolioContent
{
    public SiteMeta Site { get; set; } = new();
    public PersonInfo Person { get; set; } = new();
    public List<NavLink> NavLinks { get; set; } = new();
    public HeroSection Hero { get; set; } = new();
    public AboutSection About { get; set; } = new();
    public ExperienceSection Experience { get; set; } = new();
    public ExpertiseSection Expertise { get; set; } = new();
    public DataPlatformSection DataPlatform { get; set; } = new();
    public EducationSection Education { get; set; } = new();
    public ContactSection Contact { get; set; } = new();
    public FooterInfo Footer { get; set; } = new();
}

public class SiteMeta
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThemeColor { get; set; } = "#0b1220";
}

public class PersonInfo
{
    public string FullName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string RoleLine { get; set; } = string.Empty;
    public string PhoneDisplay { get; set; } = string.Empty;
    public string PhoneE164 { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ResumeUrl { get; set; } = string.Empty;
    public string YearsExperience { get; set; } = string.Empty;
    public string WhatsappMessage { get; set; } = string.Empty;
}

public class NavLink
{
    public string Href { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public class StatItem
{
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}

public class HeroSection
{
    public string Eyebrow { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string TerminalTitle { get; set; } = string.Empty;
    public string TerminalResult { get; set; } = string.Empty;
    public List<StatItem> Stats { get; set; } = new();
}

public class FocusArea
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class MetricItem
{
    public string Label { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class HighlightCard
{
    public string Label { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<MetricItem> Metrics { get; set; } = new();
}

public class AboutSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string LeadText { get; set; } = string.Empty;
    public List<FocusArea> FocusAreas { get; set; } = new();
    public HighlightCard Highlight { get; set; } = new();
}

public class ResponsibilityItem
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ExperienceRole
{
    // "chetu" or "default" — maps to an accent color modifier class in CSS.
    public string Accent { get; set; } = "default";
    public string Company { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ClientLabel { get; set; } = string.Empty;
    public string ClientIcon { get; set; } = "bi-geo-alt";
    public string DateRange { get; set; } = string.Empty;
    public List<string> Environment { get; set; } = new();
    public List<ResponsibilityItem> Responsibilities { get; set; } = new();
}

public class ExperienceSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<ExperienceRole> Roles { get; set; } = new();
}

public class SkillCard
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}

public class ExpertiseSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public List<SkillCard> Skills { get; set; } = new();
}

public class FlowNode
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
}

public class PlatformCard
{
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class DataPlatformSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public List<FlowNode> Flow { get; set; } = new();
    public List<PlatformCard> Cards { get; set; } = new();
}

public class EducationItem
{
    public string Icon { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class EducationSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public List<EducationItem> Items { get; set; } = new();
}

public class ContactSection
{
    public string Kicker { get; set; } = string.Empty;
    public string Heading { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
}

public class FooterInfo
{
    public string Tagline { get; set; } = string.Empty;
    public string CopyrightYear { get; set; } = string.Empty;
}
