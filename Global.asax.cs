using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MyDemoWebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static UdpListener _udpListener;
        private static Thread _listenerThread;

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 启动 UDP 监听服务
            StartUdpListener();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // 停止 UDP 监听服务
            StopUdpListener();
        }

        private void StartUdpListener()
        {
            try
            {
                _udpListener = new UdpListener();
                _listenerThread = new Thread(() => _udpListener.Start());
                _listenerThread.IsBackground = true; // 设置为后台线程，当主程序退出时自动结束
                _listenerThread.Start();

                Application["UdpListener"] = _udpListener;
            }
            catch (Exception ex)
            {
                // 记录日志
                System.Diagnostics.Trace.TraceError($"启动UDP监听失败: {ex.Message}");
            }
        }

        private void StopUdpListener()
        {
            _udpListener?.Stop();
            _listenerThread?.Join(5000); // 等待线程结束，最多5秒
        }
    }
}
