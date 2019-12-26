using System;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 3231);
            listener.Start();
            Console.WriteLine("Сервер запущен");

            while(true)
            {
                using (var client = listener.AcceptTcpClient())
                {
                    Console.WriteLine("Входящее соединение");
                    using (var stream = client.GetStream())
                    {
                        var resultText = string.Empty;
                        

                        while(stream.DataAvailable)
                        {
                            var buffer = new byte[24];
                            stream.Read(buffer, 0, buffer.Length);

                            resultText += System.Text.Encoding.UTF8.GetString(buffer);
                        }
                        Console.WriteLine($"Данные от клиента - {resultText}");
                        var answerData = System.Text.Encoding.UTF8.GetBytes("Запрос получен");
                        stream.Write(answerData, 0, answerData.Length);
                    }
                }
                Console.WriteLine("Cоединение закрыто");
            }
        }
    }
}
