# S Srinivas Rao — SQL Server Developer Portfolio

Professional SQL Server / Data Integration portfolio built with ASP.NET Core 8 MVC.

## Project structure

- `Data/portfolio-content.json` — all page copy (profile, experience, skills,
  education, contact) lives here. Edit this file to update the site; no
  Razor or C# changes are needed for content updates.
- `Models/PortfolioContent.cs` — strongly-typed C# models matching the JSON
  shape above.
- `Services/PortfolioContentService.cs` — loads and caches the JSON content
  once at startup.
- `Controllers/HomeController.cs` — serves the page and handles the contact
  form POST.
- `Views/Home/Index.cshtml` — renders the page by looping over the content
  model; contains no hardcoded resume data.
- `Views/Shared/_Layout.cshtml` — shared header/nav/footer, also driven by
  the same content model.
- `wwwroot/css/site.css` — single consolidated stylesheet (one definition
  per component, consistent breakpoints).

## Resume profile
- 3+ years of SQL Server development experience
- T-SQL, Stored Procedures, Views, Functions, Triggers
- Query optimization, execution plans, indexing, fragmentation, blocking and deadlocks
- SSIS, SSRS, Power BI and Azure Data Factory
- Azure SQL Database, Azure Data Warehouse, Azure Storage and Azure Managed Instance
- PowerShell and dbatools for automation
- 24x7 production/on-call support

## Contact
- Email: svsrinivasarao2001@gmail.com
- Phone / WhatsApp: +91 79953 98242
- WhatsApp opens with a pre-filled professional message.
- The Contact section includes a working form (posts to `HomeController.Contact`)
  in addition to the WhatsApp/email/phone quick-links.

## Run locally

```bash
dotnet restore
dotnet run
```

Open the local URL printed by ASP.NET Core.

To receive contact-form emails, set `EmailSettings` (Host, Port, UserName,
Password, From, To) in `appsettings.json` or via environment variables /
user-secrets — this section is intentionally left out of source control.

## Deployment
Compatible with Render, Azure App Service, Railway and other ASP.NET Core hosting providers.
