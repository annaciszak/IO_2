using System;
using System.IO;

namespace IO_2
{
    public class Baza
    {
        private string filename;
        /// <summary>
        /// konstruktor Baza. Czyści danę znajdujące się w bazie przed uruchomieniem aplikacji
        /// </summary>
        /// <param name="filename"></param>
        public Baza(string filename)
        {
            File.WriteAllText(filename, string.Empty);
            
            this.filename = filename;
        }
     /// <summary>
     /// Dodawanie użytkownika do bazy.
     /// </summary>
     /// <param name="admin"> nazwa użytkownika</param>
     /// <param name="password">hasło użytkownika</param>
     /// <returns>true lub false w zależności od powodzenia operacji</returns>
        public bool Insert(string admin, string password)
        {
            using (StreamWriter writetext = File.AppendText(this.filename))
            {
                writetext.WriteLine(admin + " " + password);
            return true;
            }
            return false;
        }

        /// <summary>
        /// Sprawdzanie czy wpisane hasło jest poprawne dla danego użytkownika
        /// </summary>
        /// <param name="admin">nazwa użytkownika</param>
        /// <param name="password">hasło uzytkownika</param>
        /// <returns></returns>
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
