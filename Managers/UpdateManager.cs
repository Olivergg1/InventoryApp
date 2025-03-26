
/** 

  UpdateManager

  Responsible for keeping the app up-to-date.

**/

namespace InventoryApp.Managers;

public class UpdateManager
{
/*
  static async Task<string> GetLatestGitHubVersion()
  {
    
    try
    {
        string apiUrl = $"https://api.github.com/repos/{RepoOwner}/{RepoName}/releases/latest";
        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.UserAgent.ParseAdd("request"); // Required for GitHub API

        string jsonResponse = await client.GetStringAsync(apiUrl);
        using JsonDocument doc = JsonDocument.Parse(jsonResponse);
        string latestVersion = doc.RootElement.GetProperty("tag_name").GetString();

        return latestVersion;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error checking for updates: {ex.Message}");
        return null;
    }
    
  }
  */

}
