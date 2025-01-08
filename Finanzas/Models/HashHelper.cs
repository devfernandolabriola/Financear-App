using System.Security.Cryptography;
using System.Text;

namespace Finanzas.Models
{
    public class HashHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convertir la clave en bytes
                var bytes = Encoding.UTF8.GetBytes(password);
                // Generar el hash
                var hash = sha256.ComputeHash(bytes);
                // Convertir el hash en string hexadecimal
                var hashString = new StringBuilder();
                foreach (var b in hash)
                {
                    hashString.Append(b.ToString("x2"));
                }
                return hashString.ToString();
            }
        }
    }
}
