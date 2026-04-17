using System.Net.Http.Headers;
using System.Text;

var pat = Environment.GetEnvironmentVariable("AZURE_DEVOPS_PAT")
    ?? throw new InvalidOperationException("AZURE_DEVOPS_PAT environment variable is not set.");

var org = Environment.GetEnvironmentVariable("AZURE_DEVOPS_ORG")
    ?? throw new InvalidOperationException("AZURE_DEVOPS_ORG environment variable is not set.");

var project = Environment.GetEnvironmentVariable("AZURE_DEVOPS_PROJECT")
    ?? throw new InvalidOperationException("AZURE_DEVOPS_PROJECT environment variable is not set.");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("AzureDevOps", client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}")));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

const string AzureBase = "https://dev.azure.com";
const string ApiVersion = "api-version=5.1";

string RunsUri() =>
    $"{AzureBase}/{org}/{Uri.EscapeDataString(project)}/_apis/test/runs?{ApiVersion}";

string RunUri(string id) =>
    $"{AzureBase}/{org}/{Uri.EscapeDataString(project)}/_apis/test/runs/{id}?{ApiVersion}";

app.MapGet("/api/config", () => Results.Json(new { org, project }));

app.MapGet("/api/testruns", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("AzureDevOps");
    var response = await client.GetAsync(RunsUri());
    var json = await response.Content.ReadAsStringAsync();
    return response.IsSuccessStatusCode
        ? Results.Content(json, "application/json")
        : Results.Problem(json, statusCode: (int)response.StatusCode);
});

app.MapDelete("/api/testruns/{id}", async (string id, IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("AzureDevOps");
    var response = await client.DeleteAsync(RunUri(id));
    return response.IsSuccessStatusCode
        ? Results.NoContent()
        : Results.Problem(await response.Content.ReadAsStringAsync(), statusCode: (int)response.StatusCode);
});

app.Run();
