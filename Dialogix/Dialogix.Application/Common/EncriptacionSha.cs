using System;
using System.Security.Cryptography;
using System.Text;

namespace Dialogix.Application.Common
{
    public class EncriptacionSha
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000; 

        public static string EncriptarTexto(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] key = pbkdf2.GetBytes(KeySize);
                    return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
                }
            }
        }

        public static bool VerificarCoincidencia(string password, string passworReal)
        {
            var partes = passworReal.Split('.');
            if (partes.Length != 3) return false;

            int cantidadItems = int.Parse(partes[0]);
            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] keyStored = Convert.FromBase64String(partes[2]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, cantidadItems, HashAlgorithmName.SHA256))
            {
                byte[] cadenaBytes = pbkdf2.GetBytes(keyStored.Length);
                return CompararCadenasTotal(keyStored, cadenaBytes);
            }
        }

        private static bool CompararCadenasTotal(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diferencia = 0;
            for (int i = 0; i < a.Length; i++)
                diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }
    }
}
