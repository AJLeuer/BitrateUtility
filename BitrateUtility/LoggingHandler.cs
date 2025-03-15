namespace BitrateUtility;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class LoggingHandler : DelegatingHandler
{
    public HttpRequestMessage? LastRequest { get; private set; }

    public LoggingHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Save a copy of the request
        LastRequest = CloneHttpRequestMessage(request);

        // Continue with the request
        return await base.SendAsync(request, cancellationToken);
    }

    // Helper to clone the HttpRequestMessage
    private HttpRequestMessage CloneHttpRequestMessage(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        // Copy the request's content (if any)
        if (request.Content != null)
        {
            clone.Content = new StringContent(request.Content.ReadAsStringAsync().Result);
            // Copy content headers
            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.Add(header.Key, header.Value);
            }
        }

        // Copy the request headers
        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // Optionally copy the properties
        foreach (var option in request.Options)
        {
            clone.Options.TryAdd(option.Key, option.Value);
        }

        return clone;
    }
}
