using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fbtc.Infra.Helpers
{
    public static class Functions
    {
        /// <summary>
        /// Função que criptografa a senha, caso ele tenha alterada pelo usuário.
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="isChangedPW"></param>
        /// <returns></returns>
        public static string CriptografaSenha(string senha)
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

        /// <summary>
        /// Função que faz a validação da senha do usuário para o Login
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool ValidaSenha(string senha, string hash)
        {
            bool _isValid = false;

            if (!string.IsNullOrEmpty(senha) && !string.IsNullOrEmpty(hash))
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    _isValid = VerifyMd5Hash(md5Hash, senha, hash);
                }
            }
            return _isValid;
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

        public static string GetNovaSenhaAcesso(string ch)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            string Letras = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ";

            builder.Clear();

            builder.Append(ch);
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(random.Next(0, 9).ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            builder.Append(Letras[random.Next(0, Letras.Length - 1)].ToString());
            return builder.ToString();
        }
    }
}
