using System.Net;

namespace Aws.Demo.Api.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessCode(this HttpStatusCode httpStatusCode)
        {
            var code = (int)httpStatusCode;

            return code >= 200 && code < 300;
        }
    }
}
