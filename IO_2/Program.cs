using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IO_2
{
    class Program { 
    static bool checkPort(int port){
            if (port < 1024 || port > 49151)
            {
                return false;
            }
            return true;
    }
        static void Main(string[] args)
        {

            int port = 0;
            bool check = false;
            Regex reg = new Regex("[^0-9]");
            while (check != true)
            {
                Console.WriteLine("Enter port: ");
                string p = Console.ReadLine(); 
                p = reg.Replace(p, ""); 
                port = int.Parse(p);
                if (!checkPort(port)) Console.WriteLine("Incorrect port number!\n");
                else check = true;

            }
            Server server = new Server(port);
            server.Start();


        }
    }
}
