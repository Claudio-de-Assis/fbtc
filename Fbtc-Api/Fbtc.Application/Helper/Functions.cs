
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
    }
}
