using System;
using System.Net;
using System.Net.Sockets;

namespace PasswordCrackerCentralized
{
    class Program
    {
        static void Main()
        {
            int _port = 65080;
            IPAddress _ip = IPAddress.Parse("10.154.1.247");

            Console.WriteLine("--- Waiting for Connection ---");

            TcpListener server = new TcpListener(_ip, _port);
            server.Start();

            while (true)
            {
                ServiceServer service = new ServiceServer(server.AcceptTcpClient());

                service.DoTheCracking();


            }


        }
    }
}
