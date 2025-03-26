using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;

namespace InventoryApp.Helpers;

public class ProtectedStringConverter : JsonConverter<string>
{
  private static IDataProtector? _protector;

  // Allow setting the protector globally
  public static void SetProtector(IDataProtector protector)
  {
    _protector = protector;
  }

  public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer)
  {
    if (_protector == null || string.IsNullOrEmpty(value))
    {
      writer.WriteValue(value);
    }
    else
    {
      writer.WriteValue(_protector.Protect(value));
    }
  }

  public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer)
  {
    if (_protector == null || reader.Value == null)
    {
      return reader.Value?.ToString();
    }

    try
    {
      return _protector.Unprotect(reader.Value.ToString());
    }
    catch
    {
      return reader.Value.ToString(); // If decryption fails, return raw value
    }
  }
}