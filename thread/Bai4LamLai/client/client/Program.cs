using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Console.Write("Server IP address: ");
              var serverIpStr = Console.ReadLine();
            // chuyển đổi chuỗi ký tự thành object thuộc kiểu IPAddress
            var serverIp = IPAddress.Parse(serverIpStr);
            // yêu cầu người dùng nhập cổng của server
              Console.Write("Server port: ");
              var serverPortStr = Console.ReadLine();
            // chuyển chuỗi ký tự thành biến kiểu int
            var serverPort = int.Parse(serverPortStr);
            // đây là "địa chỉ" của tiến trình server trên mạng
            // mỗi endpoint chứa ip của host và port của tiến trình
            var serverEndpoint = new IPEndPoint(serverIp, serverPort);
            var size = 1024; // kích thước của bộ đệm
            var receiveBuffer = new byte[size]; // mảng byte làm bộ đệm            
         
                // khởi tạo object của lớp socket để sử dụng dịch vụ Tcp
                // lưu ý SocketType của Tcp là Stream 
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                // tạo kết nối tới Server

                socket.Connect(serverEndpoint);
                // gửi mảng byte trên đến tiến trình server

                // không tiếp tục gửi dữ liệu nữa

                // nhận mảng byte từ dịch vụ Tcp và lưu vào bộ đệm                    
                var length = socket.Receive(receiveBuffer);
                // chuyển đổi mảng byte về chuỗi
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                // xóa bộ đệm (để lần sau sử dụng cho yên tâm)
                Array.Clear(receiveBuffer, 0, size);
                // không tiếp tục nhận dữ liệu nữa
                socket.Shutdown(SocketShutdown.Receive);
                // đóng socket và giải phóng tài nguyên
                socket.Close();
                // in kết quả ra màn hình
                Console.WriteLine($">>> {result}");
    
            Console.ReadKey();
        }
    }
}
