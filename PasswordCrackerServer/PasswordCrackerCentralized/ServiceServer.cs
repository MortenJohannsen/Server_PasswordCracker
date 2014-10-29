using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordCrackerCentralized
{
    public class ServiceServer
    {
        private TcpClient connectionSocket;
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;
        private Cracking crackService;
        private FileStream fs;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connection">Connection initiated from TCP Listener</param>
        public ServiceServer(TcpClient connection)
        {
            connectionSocket = connection;
            Console.WriteLine("\n--- Server Activated ---");
            crackService = new Cracking();
        }

        public void GetDictionary()
        {
            ns = connectionSocket.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            sw.AutoFlush = true; //Enable automatic flushing


            List<string> DictionaryRecieved = new List<string>();
            bool x = true;
            while (x)
            {
               string recieved = sr.ReadLine();
                DictionaryRecieved.Add(recieved);
                Console.WriteLine(recieved);
                if (recieved == "kernel")
                {
                    x = false;
                }
            }
            
            File.WriteAllLines("webster.txt", DictionaryRecieved.ToArray());

            Console.WriteLine("--- File Recieved --- " + DictionaryRecieved);

            

            Console.WriteLine("\n--- Closing connection ---");

            ns.Close();
            connectionSocket.Close();
        }



        /// <summary>
        /// This method is reading the password sent from the client machine, and makes use of the CrackingClass
        /// </summary>
        public void DoTheCracking()
        {

            ns = connectionSocket.GetStream();
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            sw.AutoFlush = true; //Enable automatic flushing


            List<string> PasswordsRecieved = new List<string>();
            bool x = true;
            while (x)
            {
                string recieved = sr.ReadLine();
                PasswordsRecieved.Add(recieved);
                Console.WriteLine(recieved);
                if (recieved == "per:AXPaVO/3DmqNsW2uPJw9ZJxf9lc=")
                {
                    x = false;
                }
            }

            File.WriteAllLines("passwords.txt", PasswordsRecieved.ToArray());
            



            //string messagerecieved = sr.ReadLine();

            //File.WriteAllText("passwords.txt", messagerecieved);

            //Console.WriteLine("--- File Recieved --- " + messagerecieved);
            
            Console.WriteLine("Run Cracker...");
            crackService.RunCracking();
            Console.WriteLine("Cracking Done");
            
            //Send file to Client
            string file = File.ReadAllText(
                @"C:\Users\Morten Johannsen\Desktop\School\3. Semester\Password_Cracker_Assignment\PasswordCrackerServer\PasswordCrackerServer\PasswordCrackerCentralized\bin\Debug\password_results.txt");
            Console.WriteLine("--- Sending result to client ---");
            sw.Write("Morten's Server: " + file);
            Console.WriteLine("--- File Successfully sent ---");



            Console.WriteLine("\n--- Press any key to close ---");

            ns.Close();
            connectionSocket.Close();

        }

    }//end of class
}//end of namespace
