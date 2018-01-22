using System;
using System.Text;
using System.Web;

namespace Custom.ServerModules
{
    public class CustomHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose()
        {

        }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            TryRemoveResponseHeader("Server");
            TryRemoveResponseHeader("X-AspNet-Version");
            TryRemoveResponseHeader("X-Powered-By");
        }

        private void TryRemoveResponseHeader(String header)
        {
            try
            {
                var isExists = HttpContext.Current.Response.Headers[header] != null;
                if (isExists)
                    HttpContext.Current.Response.Headers.Remove(header);
            }
            catch { }
        }
    }
}