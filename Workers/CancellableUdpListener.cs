using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class CancellableUdpListener
{
    public async Task StartWithCancellationAsync(int port, CancellationToken cancellationToken = default)
    {
        using (UdpClient udpClient = new UdpClient(port))
        {
            Console.WriteLine($"开始监听UDP端口 {port}...");

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // 创建带取消功能的接收任务
                    var receiveTask = udpClient.ReceiveAsync();
                    var timeoutTask = Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                    
                    var completedTask = await Task.WhenAny(receiveTask, timeoutTask);
                    
                    if (completedTask == receiveTask)
                    {
                        UdpReceiveResult result = await receiveTask;
                        string message = Encoding.UTF8.GetString(result.Buffer);
                        
                        Console.WriteLine($"收到消息: {message} 来自 {result.RemoteEndPoint}");
                        
                        // 处理消息
                        await ProcessMessageAsync(message, result.RemoteEndPoint, udpClient);
                    }
                    // 如果超时，继续循环检查取消令牌
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("监听已被取消");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"监听错误: {ex.Message}");
            }
        }
    }

    private async Task ProcessMessageAsync(string message, IPEndPoint remoteEndPoint, UdpClient udpClient)
    {
        // 异步处理消息
        await Task.Run(() =>
        {
            // 模拟处理时间
            Thread.Sleep(100);
            Console.WriteLine($"处理完成: {message}");
        });

        // 发送回复
        string reply = $"处理完成: {message}";
        byte[] replyBytes = Encoding.UTF8.GetBytes(reply);
        await udpClient.SendAsync(replyBytes, replyBytes.Length, remoteEndPoint);
    }
}