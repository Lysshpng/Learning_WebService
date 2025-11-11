using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class AsyncUdpListener
{
    private UdpClient _udpClient;
    private bool _isListening;

    public async Task StartAsync(int port)
    {
        _udpClient = new UdpClient(port);
        _isListening = true;

        Console.WriteLine($"开始异步监听UDP端口 {port}...");

        try
        {
            while (_isListening)
            {
                // 异步接收数据
                UdpReceiveResult result = await _udpClient.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                
                Console.WriteLine($"收到来自 {result.RemoteEndPoint} 的消息: {message}");
                
                // 处理消息（可以在后台处理，不阻塞接收）
                _ = Task.Run(() => ProcessMessage(message, result.RemoteEndPoint));
            }
        }
        catch (ObjectDisposedException)
        {
            // 正常关闭时的异常
        }
        catch (Exception ex)
        {
            Console.WriteLine($"接收错误: {ex.Message}");
        }
    }

    private void ProcessMessage(string message, IPEndPoint remoteEndPoint)
    {
        // 在这里处理接收到的消息
        Console.WriteLine($"处理消息: {message}");
        
        // 示例：发送回复
        try
        {
            string reply = $"已收到你的消息: {message}";
            byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
            _udpClient.Send(replyBytes, replyBytes.Length, remoteEndPoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发送回复失败: {ex.Message}");
        }
    }

    public void Stop()
    {
        _isListening = false;
        _udpClient?.Close();
        Console.WriteLine("UDP监听已停止");
    }
}