namespace BitrateUtility.Source.Client.Utility;

public class LoggingHandler : DelegatingHandler
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public HttpRequestMessage? LastRequest { get; private set; }

    public LoggingHandler(HttpMessageHandler innerHandler) : base(innerHandler) { }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = CloneHttpRequestMessage(request);
        return await base.SendAsync(request, cancellationToken);
    }
    
    private HttpRequestMessage CloneHttpRequestMessage(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content != null)
        {
            clone.Content = new StringContent(request.Content.ReadAsStringAsync().Result);
            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.Add(header.Key, header.Value);
            }
        }

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
        
        foreach (var option in request.Options)
        {
            clone.Options.TryAdd(option.Key, option.Value);
        }

        return clone;
    }
}
