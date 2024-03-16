using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sever
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Tcp Server";
            // giá trị Any của IPAddress tương ứng với Ip của tất cả các giao diện mạng trên máy
            var localIp = IPAddress.Any;
            // tiến trình server sẽ sử dụng cổng tcp 1308
            var localPort = 1308;
            // biến này sẽ chứa "địa chỉ" của tiến trình server trên mạng
            var localEndPoint = new IPEndPoint(localIp, localPort);
            // tcp sử dụng đồng thời hai socket: 
            // một socket để chờ nghe kết nối, một socket để gửi/nhận dữ liệu
            // socket listener này chỉ làm nhiệm vụ chờ kết nối từ Client
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // yêu cầu hệ điều hành cho phép chiếm dụng cổng tcp 1308
            // server sẽ nghe trên tất cả các mạng mà máy tính này kết nối tới
            // chỉ cần gói tin tcp đến cổng 1308, tiến trình server sẽ nhận được
            listener.Bind(localEndPoint);
            // bắt đầu lắng nghe chờ các gói tin tcp đến cổng 1308
            listener.Listen(10);
            Console.WriteLine($"Local socket bind to {localEndPoint}. Waiting for request ...");
            var size = 1024;
            var receiveBuffer = new byte[size];
            while (true)
            {
                var socket = listener.Accept();
                if (socket.Connected==true) 
                {
                    Thread t = new Thread(() => {
                        try
                        {
                            Console.WriteLine($"Accepted connection from {socket.RemoteEndPoint}");
                            string str = GetCurrentTimeString();
                            byte[] response = Encoding.ASCII.GetBytes(str);
                            socket.Send(response);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error handling connection: {ex.Message}");
                        }
                        finally
                        {
                            socket.Shutdown(SocketShutdown.Send);
                            socket.Close();
                        }
                    });
                    t.Start();
                }
               
            }
        }
        public static string GetCurrentTimeString()
        {
            // Lấy thời gian hiện tại
            DateTime currentTime = DateTime.Now;

            // Chuyển đổi thời gian thành chuỗi theo định dạng mong muốn (ví dụ: "yyyy-MM-dd HH:mm:ss")
            string currentTimeString = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

            // Trả về chuỗi thời gian
            return currentTimeString;
        }

    }
}
