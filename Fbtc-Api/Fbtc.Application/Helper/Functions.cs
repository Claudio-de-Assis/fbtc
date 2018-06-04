using System;
using System.Text;

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
    }
}
