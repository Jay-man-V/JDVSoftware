using System.Net.Http;

namespace Foundation.Core
{
    public interface IHeartbeatController
    {
        HttpResponseMessage ExceptionDemo();
        HttpResponseMessage GetHeartbeat();
        HttpResponseMessage BasicExceptionDemo();
    }
}
