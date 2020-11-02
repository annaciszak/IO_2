using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace IO_2
{
    public class Server
    {
        IPAddress ip;
        TcpListener listener;
        Baza db;
        int port;
        public delegate void TransmissionDataDelegate(NetworkStream stream);
        public Server(int port)
        {
            this.port = port;
            ip = IPAddress.Loopback;
            db = new Baza("b1.txt");
            db.Insert("admin", "password");
            db.Insert("abc", "1234");
            db.Insert("marek", "12sd");
            db.Insert("kasia", "ew43c");
            listener = new TcpListener(ip, port);
        }

        public void AcceptClient()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                TransmissionDataDelegate transmissionDelegate = BeginDataTransmission;
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, client);
            }
        }

        public void TransmissionCallback(IAsyncResult res)
        {

        }

        public void BeginDataTransmission(NetworkStream stream)
        {
            byte[] login = new byte[1024];
            byte[] password = new byte[1024];
            byte[] cmd = new byte[1024];
            Regex reg = new Regex("[^a-zA-Z0-9]");
            while (true)
            {
                try
                {
                    byte[] t1 = Encoding.ASCII.GetBytes("Enter login: ");
                    byte[] t2 = Encoding.ASCII.GetBytes("Enter password: ");
                    stream.Write(t1, 0, t1.Length);
                    stream.Read(login, 0, 1024);
                    stream.Write(t2, 0, t2.Length);
                    stream.Read(password, 0, 1024);
                    string clean_login = reg.Replace(Encoding.ASCII.GetString(login, 0, login.Length), "");
                    string clean_password = reg.Replace(Encoding.ASCII.GetString(password, 0, password.Length), "");
                    bool is_good = db.Select(clean_login, clean_password);
                    if (is_good)
                    {
                        byte[] t3 = Encoding.ASCII.GetBytes("Welcome!\n");
                        stream.Write(t3, 0, t3.Length);
                        break;
                    }
                    else
                    {
                        byte[] t3 = Encoding.ASCII.GetBytes("Incorrect login or password\n");
                        stream.Write(t3, 0, t3.Length);
                    }
                }
                catch(IOException e)
                {
                    Console.Error.WriteLine(e.Message);
                    stream.Close();
                    break;
                }
            }

            while (true)
            { 
                try
                {
                    byte[] menu = Encoding.ASCII.GetBytes("\n Menu \n1.Calculate Fibonacci \n2.Exit \n");
                    stream.Write(menu, 0, menu.Length);
                    stream.Read(cmd, 0, 1024);
                    string clean_cmd = reg.Replace(Encoding.ASCII.GetString(cmd, 0, cmd.Length), "");
                    if (clean_cmd == "1")
                    {
                        Console.WriteLine(2);
                        byte[] m4 = Encoding.ASCII.GetBytes("Enter number of Fibo: ");
                        stream.Write(m4, 0, m4.Length);
                        byte[] buffer = new byte[1024];
                        stream.Read(buffer, 0, 1024);
                        string z = Encoding.UTF8.GetString(buffer, 0, 1024);
                        Console.WriteLine(z);
                        int z2 = int.Parse(z);
                        int res = Library.Start(z2);
                        Console.WriteLine(res);
                        buffer = Encoding.UTF8.GetBytes(res.ToString());
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    else if(clean_cmd == "2")
                    {
                        byte[] close = Encoding.ASCII.GetBytes("Log out\n");
                        stream.Write(close, 0, close.Length);
                        stream.Close();
                    }
                    else
                    {
                        byte[] m = Encoding.ASCII.GetBytes("Incorrect number \n");
                        stream.Write(m, 0, m.Length);
                    }
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.Message);
                    stream.Close();
                    break;
                }
            }
        } 
        public void Start()
        {
            listener.Start();
            Console.WriteLine("Listening on " + this.port);
            AcceptClient();
        }
    }
}
