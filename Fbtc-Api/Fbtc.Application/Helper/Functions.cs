using System;
using System.Text;
using System.Security.Cryptography;

namespace Fbtc.Application.Helper
{
    public static class Functions
    {
        public static string AjustaTamanhoString(string str, int tamanho)
        {
            string _str = "";
            if(!string.IsNullOrWhiteSpace(str))
                _str = str.Trim().Length > tamanho ? str.Trim().Substring(0, tamanho) : str.Trim();

            return _str;
        }

        public static string GetHashToNomeImagem(string fileName)
        {
            if (fileName.Length == 0)
                return null;

            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            string extFile = fileName.Substring(fileName.Length - 4, 4);

            string Letras = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";

            builder.Clear();

            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(extFile);
            return builder.ToString();
        }


        public static string CriptografaSenhaNova(string senha)
        {
            string _senha = senha;

            if (!string.IsNullOrEmpty(senha))
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    _senha = GetMd5Hash(md5Hash, senha);
                }
            }
            return _senha;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
