using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UdpListener
{
    private UdpClient _udpClient;
    private bool _isListening;
    private Thread _receiveThread;

    public void Start()
    {
        try
        {
            // 从 Web.config 读取配置
            string listenIp = ConfigurationManager.AppSettings["UdpListenIp"] ?? "0.0.0.0";
            int listenPort = int.Parse(ConfigurationManager.AppSettings["UdpListenPort"] ?? "8888");

            IPAddress ipAddress = IPAddress.Parse(listenIp);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, listenPort);

            _udpClient = new UdpClient(localEndPoint);
            _isListening = true;

            // 在新线程中启动监听
            _receiveThread = new Thread(ReceiveMessages);
            _receiveThread.IsBackground = true;
            _receiveThread.Start();

            System.Diagnostics.Trace.TraceInformation($"UDP监听已启动: {localEndPoint}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"启动UDP监听失败: {ex.Message}");
        }
    }

    private void ReceiveMessages()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        while (_isListening)
        {
            try
            {
                byte[] receivedBytes = _udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(receivedBytes);

                // 记录接收到的消息
                System.Diagnostics.Trace.TraceInformation($"收到UDP消息: {message} 来自 {remoteEndPoint}");

                // 处理消息（可以在后台处理）
                ThreadPool.QueueUserWorkItem(state => ProcessMessage(message, remoteEndPoint));
            }
            catch (SocketException ex)
            {
                if (_isListening) // 如果不是正常关闭导致的异常
                {
                    System.Diagnostics.Trace.TraceError($"接收UDP消息错误: {ex.Message}");
                }
                break;
            }
            catch (ObjectDisposedException)
            {
                // UDP客户端已被释放，正常退出
                break;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"处理UDP消息错误: {ex.Message}");
            }
        }
    }

    private void ProcessMessage(string message, IPEndPoint remoteEndPoint)
    {
        try
        {
            // 在这里处理业务逻辑
            System.Diagnostics.Trace.TraceInformation($"处理UDP消息: {message}");

            // 可选：发送回复
            //string reply = $"已收到: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            //byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
            //_udpClient.Send(replyBytes, replyBytes.Length, remoteEndPoint);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"处理消息失败: {ex.Message}");
        }
    }

    public void Stop()
    {
        _isListening = false;

        try
        {
            _udpClient?.Close();
            _receiveThread?.Join(3000); // 等待接收线程结束
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError($"停止UDP监听时出错: {ex.Message}");
        }

        System.Diagnostics.Trace.TraceInformation("UDP监听已停止");
    }
}