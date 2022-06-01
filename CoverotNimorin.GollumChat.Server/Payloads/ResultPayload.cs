using System.Text.Json.Serialization;

namespace CoverotNimorin.GollumChat.Server.Payloads;

public class ResultPayload
{
    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }

    public ResultPayload(string message)
    {
        Messages = new List<string>()
        {
            message
        };
    }

    public ResultPayload(List<string> messages)
    {
        Messages = messages;
    }
}

public class ResultPayload<T> : ResultPayload
{
    [JsonPropertyName("data")]
    public T? Data { get; set; } 

    public ResultPayload(T? data, string message)
        : base(message)
    {
        Data = data;
    }

    public ResultPayload(T? data, List<string> messages)
        : base(messages)
    {
        Data = data;
    }
}