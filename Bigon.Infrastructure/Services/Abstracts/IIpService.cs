namespace Bigon.Infrastructure.Services.Abstracts
{
    public interface IIpService
    {
        string GetRequestIp(bool tryUseXForwardHeader = true);
        T GetHeaderValueAs<T>(string headerName);
    }
}
