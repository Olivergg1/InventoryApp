
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace InventoryApp.Helpers;

public record SearchOptions(string Prompt = "", int Limit = 15) {}

public static class PromptHelper
{
  private static readonly SearchOptions _defaultSearchOptions = new("Search", 15);

  public static string? Prompt(string prompt)
  {
    Console.Write($"\u001b[36m{prompt}:\u001b[0m ");
    
    return Console.ReadLine();
  }

  public static int? PromptInt(string prompt)
  {
    var raw = Prompt(prompt) ?? string.Empty;
    
    if (int.TryParse(raw, out var result)) return result;

    return null;
  }

  public static string? Pass(string prompt)
  {
    var password = string.Empty;

    Console.Write($"\u001b[36m{prompt}:\u001b[0m ");

    while (true)
    {
      var keyInfo = Console.ReadKey(intercept: true);

      if (keyInfo.Key == ConsoleKey.Enter)
      {
        Console.Write("\n");
        break;
      };

      if (keyInfo.Key == ConsoleKey.Backspace) {
        if (password.Length > 0)
        {
          password = password[..^1];
          Console.Write("\b \b");
        }
      }
      else
      {
        password += keyInfo.KeyChar;
        Console.Write("*");
      }
    }

    return password;
  }

  public static T? Search<T>(IEnumerable<T> items, Func<T, string, bool> predicate, SearchOptions? options = null) {
    
    // If SearchOptions is null, assign default options
    options ??= _defaultSearchOptions;

    // Stored search results
    IEnumerable<T> filteredItems = [];

    // String used in search
    var searchTerm = new StringBuilder();

    var (left, top) = Console.GetCursorPosition();
    var promptCursorOffset = top + 1;
    var resultsCursorOffset = top + 2;

    var selected = 0;

    var MoveSelectedUp = () => {
      selected = Math.Max(0, selected - 1);
    };

    var MoveSelectedDown = () => {
      var upbound = Math.Min(options.Limit - 1, Math.Max(0, filteredItems.Count() - 1)); 
      selected = Math.Min(upbound, selected + 1);
    };

    while (true)
    {
      selected = Math.Min(Math.Max(0, filteredItems.Count() - 1), selected);
      
      // Clear list of results
      Console.SetCursorPosition(0, resultsCursorOffset);
      foreach (var _ in new int[options.Limit]) Console.Out.WriteLine(new string(' ', 80));

      // Reset cursor position after clearing list
      Console.SetCursorPosition(0, resultsCursorOffset);

      filteredItems = items.Where(item => predicate(item, searchTerm.ToString())).Take(options.Limit) ?? [];

      if (!filteredItems.Any()) Console.Out.WriteLine("No search results");

      // Write newly filtered items
      foreach (var (index, item) in filteredItems.Select((item, i) => (i, item)))
      {
        if (index == selected) Console.Out.Write($"> \u001b[97m");

        Console.Out.WriteLine($"{item}\u001b[0m");
      }
      
      Console.SetCursorPosition(0, top);
      Console.WriteLine("Start typing to search. Press 'Enter' to confirm or 'Esc' to exit.");
      Console.Write($"\u001b[36m{options.Prompt}:\u001b[0m " + searchTerm.ToString());
      Console.SetCursorPosition(options.Prompt.Length + 2 + searchTerm.Length, promptCursorOffset);

      var key = Console.ReadKey(intercept: true);

      if (key.Key == ConsoleKey.Escape) return default;
      if (key.Key == ConsoleKey.Enter) break;
      if (key.Key == ConsoleKey.UpArrow) MoveSelectedUp();
      if (key.Key == ConsoleKey.DownArrow) MoveSelectedDown();
      if (key.Key == ConsoleKey.Backspace && searchTerm.Length > 0) 
      {
        searchTerm.Remove(searchTerm.Length - 1, 1);
        Console.Write("\b \b");
      }
      if (!char.IsControl(key.KeyChar)) searchTerm.Append(key.KeyChar);
    }

    // Clean up after search is done
    Console.SetCursorPosition(0, top);
    foreach (var _ in new int[options.Limit + 2]) Console.Out.WriteLine(new string(' ', 80));
    Console.SetCursorPosition(0, top);

    var result = filteredItems.Where((item, i) => i == selected).FirstOrDefault();

    Console.WriteLine($"\u001b[36m{options.Prompt}:\u001b[0m " + result?.ToString());

    return result;
  }
}