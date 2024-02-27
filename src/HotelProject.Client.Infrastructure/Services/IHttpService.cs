namespace Client.Infrastructure.Services;

public interface IHttpService
{
    Task<HttpResponseMessage> Get(string uri);
    Task<HttpResponseMessage> Delete(string uri);
    Task<HttpResponseMessage> Post(string uri, object value);
    Task<HttpResponseMessage> Patch(string uri, object value);
    Task<HttpResponseMessage> Put(string uri, object value);
}