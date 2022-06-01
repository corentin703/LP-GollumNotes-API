using System.Net;

namespace CoverotNimorin.GollumChat.Server.Exceptions;

public abstract class WebException : Exception
{
    public HttpStatusCode Status { get; }

    public List<string> Messages { get; }
    
    protected WebException(HttpStatusCode status, string message) : base(message)
    {
        Status = status;
        Messages = new List<string>()
        {
            message,
        };
    }
    
    protected WebException(HttpStatusCode status, List<string> messages) : base(messages.FirstOrDefault())
    {
        Status = status;
        Messages = messages;
    }
}