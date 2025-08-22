# FeatherySharp (Unofficial)

> An unofficial, lightweight .NET client for [Feathery](https://feathery.io) REST APIs.  
> **Not affiliated with Feathery.** API behavior may change without notice.

[![Build](https://github.com/dbheale/featherysharp/actions/workflows/ci.yml/badge.svg)](https://github.com/dbheale/featherysharp/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Feathery.Unofficial.Client.svg)](https://www.nuget.org/packages/Feathery.Unofficial.Client)

## Features

- Friendly, typed client over `HttpClient`
- Covers Accounts, Forms (CRUD), Submissions, Hidden Fields, Users, Logs, Workspaces, Documents, and AI runs
- Minimal dependencies (`Microsoft.Extensions.Http`)
- DI extension for one-liner setup
- Optional polling helper for “export PDF” flows

## Install

```bash
dotnet add package Feathery.Unofficial.Client
```

## Quick start

```csharp
using Feathery.Unofficial.Client;
using Feathery.Unofficial.Client.Models;

var builder = WebApplication.CreateBuilder(args);

// Reads configuration from "Feathery" section and/or FEATHERY_API_KEY env var
builder.AddFeathery();

var app = builder.Build();

// Example endpoint: list forms
app.MapGet("/forms", async (IFeatheryClient client) =>
{
    var forms = await client.ListFormsAsync();
    return Results.Ok(forms);
});

app.Run();
```

### Configuration

Add to `appsettings.json` (or set `FEATHERY_API_KEY`):

```json
{
  "Feathery": {
    "ApiKey": "YOUR_ADMIN_API_KEY",
    "BaseUri": "https://api.feathery.io/",
    "PdfPollAttempts": 5,
    "PdfPollDelay": "00:00:01"
  }
}
```

The DI extension validates `ApiKey` on startup.

## Usage examples

```csharp
// Resolve via DI
public sealed class MyService(IFeatheryClient feathery)
{
    public async Task DemoAsync()
    {
        // Accounts
        var account = await feathery.GetAccountAsync();
        // Forms
        var forms = await feathery.ListFormsAsync();
        var schema = await feathery.GetFormSchemaAsync("my_form_name_or_id");
        // Submissions
        var upsert = await feathery.CreateOrUpdateSubmissionAsync(new CreateOrUpdateSubmissionRequest {
            UserId = "user@example.com",
            Forms = ["My Form"],
            Fields = new() { ["age"] = 42 },
            Complete = true
        });
        // Export PDF (waits until ready via HEAD polling)
        var pdf = await feathery.ExportSubmissionPdfAsync("<FORM_ID>", "user@example.com");
    }
}
```

## Supported endpoints (high level)

- **Accounts**: get, edit, invite/uninvite, rotate key
- **Forms**: list, get schema, create, update, delete (with confirm), copy
- **Submissions**: create/update, batch list, export PDF
- **Hidden Fields**: create
- **Users**: list, get fields, get session, create/update, delete
- **Logs**: API connector errors, recent emails, email issues, Quik requests
- **Workspaces**: list, create, get, update, delete, auth token, create template form
- **Documents**: fill/sign, list envelopes, delete envelope
- **AI**: run extraction (multipart), list runs

> For endpoint semantics, see Feathery’s official docs: https://api-docs.feathery.io/

## Exceptions & diagnostics

API errors throw `FeatheryApiException` with:
- `StatusCode`
- `ResponseBody` (string; useful for debugging)
- `Request` (the `HttpRequestMessage` that failed)

Add standard ASP.NET Core logging for request/response tracing if needed.

## Compatibility

- Targets: `net8.0`, `net6.0`
- Dependencies: `Microsoft.Extensions.Http`, `Microsoft.Extensions.DependencyInjection.Abstractions`, `Microsoft.Extensions.Hosting.Abstractions`, `Microsoft.Extensions.Http`, `Microsoft.Extensions.Options.ConfigurationExtensions`, `Microsoft.SourceLink.GitHub`

## Contributing

PRs welcome! Please:
1. Create an issue describing the change.
2. Run unit tests locally (`dotnet test`).
3. For integration tests, set `FEATHERY_API_KEY` (these are skipped by default).

## Release & versioning

- **SemVer** (0.x while we stabilize)
- Create a Git tag `vX.Y.Z` on `main` to publish to NuGet via GitHub Actions.

## Security

Do **not** include real API keys in issues or PRs. Report sensitive bugs privately if needed.

## License

MIT © 2025 David Heale

---

**Disclaimer**: This is an **unofficial** SDK and not endorsed by Feathery.
