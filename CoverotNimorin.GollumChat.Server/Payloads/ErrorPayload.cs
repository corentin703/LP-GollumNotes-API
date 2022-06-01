using System.Text.Json.Serialization;

namespace CoverotNimorin.GollumChat.Server.Payloads;

public class ErrorPayload
{
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }
    
    public ErrorPayload(string error)
    {
        Errors = new List<string>()
        {
            error
        };
    }

    public ErrorPayload(List<string> errors)
    {
        Errors = errors;
    }
}