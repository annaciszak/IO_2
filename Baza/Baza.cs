using System;
using System.IO;

namespace IO_2
{
    public class Baza
    {
        private string filename;
        public Baza(string filename)
        {
            File.WriteAllText(filename, string.Empty);
            
            this.filename = filename;
        }
        public bool Insert(string admin, string password)
        {
            using (StreamWriter writetext = File.AppendText(this.filename))
            {
                writetext.WriteLine(admin + " " + password);
            return true;
            }
            return false;
        }

        public bool Select(string admin, string password)
        {
            using (StreamReader readtext = new StreamReader(this.filename))
            {
                string s;
                string[] line;
                while ((s = readtext.ReadLine()) != null)
                {
                    line = s.Split(' ');
                    if (line[0] == admin && line.Length == 2)
                    {
                        if (line[1] == password) return true;
                        else return false;
                    }
                }
            }
            return false;
        }


    }
}
