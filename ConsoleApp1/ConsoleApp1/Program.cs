using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Printing;


using System.Diagnostics;
namespace StorePassword
{
    class Program
    {
        public static bool DEBUG { get; private set; } = true;

        private static int numberOfIterations = 10000;

        public class SaltAndHash
        {
            public string salt { get; set; }

            public string hash { get; set; }

            public SaltAndHash(string salt, string hash)
            {
                this.salt = salt;
                this.hash = hash;
            }
        }

        static void Main(string[] args)
        {
            
         
            string password = "ASDF1234MN";
          
            Stopwatch sw = new Stopwatch();
           
                
                SaltAndHash sh = GenerateSaltAndHash(password);
              
                Console.WriteLine(sw.ElapsedMilliseconds);
                Console.WriteLine($"Salt: {sh.salt}");
                Console.WriteLine($"Hash: {sh.hash}");
                Console.WriteLine($"Salt: {sh.salt.Length}");
                Console.WriteLine($"Hash: {sh.hash.Length}");

            byte[] saltBytes = Convert.FromBase64String(sh.salt);

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password,saltBytes);

            rfc2898DeriveBytes.IterationCount = numberOfIterations;
            byte[] enteredHash = rfc2898DeriveBytes.GetBytes(20);
            // Umwandeln von byte-Array in String
            string str = Convert.ToBase64String(enteredHash);

            Console.WriteLine(str);

            Console.ReadLine();
        }

        // Überprüfen eines Passworts
     
        public static SaltAndHash GenerateSaltAndHash(string password)
        {
            // Bibliotheksklasse zum Erzeugen eines Hash-Wertes und eines Salt-Wertes
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 32);
            // Anzahl der Iterationen (erhöht den Rechenaufwand)
            rfc2898DeriveBytes.IterationCount = numberOfIterations;
            // Auslesen des generierten Hash-Wertes
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            // Auslesen des generierten Salt-Wertes
            byte[] salt = rfc2898DeriveBytes.Salt;

            // Umwandeln von einem byte-Array in einen String 
            string saltString = Convert.ToBase64String(salt);
            string passwordHash = Convert.ToBase64String(hash);
            // Ein Array mit Salt- und Hash-Wert werden zurück gegeben
            return new SaltAndHash(saltString, passwordHash);
        }


     
      
    }
}
