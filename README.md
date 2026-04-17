# test_run_deleter
Pseudo fork of: https://www.opentechguides.com/how-to/article/azure/207/rest-api-delete-test-run.html

A minimal ASP.NET Core web app for browsing and deleting Azure DevOps test runs via the REST API.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An Azure DevOps account with access to the target organization and project
- A [Personal Access Token (PAT)](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate) with **Test Management: Read & Write** permissions

## Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/st0k3r/test_run_deleter.git
   cd test_run_deleter
   ```

2. **Set the required environment variables**

   | Variable | Description |
   |---|---|
   | `AZURE_DEVOPS_PAT` | Your Azure DevOps Personal Access Token |
   | `AZURE_DEVOPS_ORG` | Your Azure DevOps organization name |
   | `AZURE_DEVOPS_PROJECT` | The project name to manage test runs for |

   On Linux/macOS:
   ```bash
   export AZURE_DEVOPS_PAT=your_pat_here
   export AZURE_DEVOPS_ORG=your_org_name
   export AZURE_DEVOPS_PROJECT=your_project_name
   ```

   On Windows (PowerShell):
   ```powershell
   $env:AZURE_DEVOPS_PAT="your_pat_here"
   $env:AZURE_DEVOPS_ORG="your_org_name"
   $env:AZURE_DEVOPS_PROJECT="your_project_name"
   ```

   Alternatively, create a `.env` file in the project root (it is gitignored):
   ```
   AZURE_DEVOPS_PAT=your_pat_here
   AZURE_DEVOPS_ORG=your_org_name
   AZURE_DEVOPS_PROJECT=your_project_name
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. Open your browser to `http://localhost:5000` (or the port shown in the terminal output).
