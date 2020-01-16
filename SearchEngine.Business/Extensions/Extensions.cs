using PublicResXFileCodeGenerator;
using System.Text.RegularExpressions;

namespace SearchEngine.Business.Extensions
{
    public static class Extensions
    {
        public static string RemoveWrongSymbolsFromYandexResponse(this string response)
        {
            response = response.Replace(@"<hlword>", string.Empty).Replace(@"</hlword>", string.Empty);
            response = Regex.Replace(response, @"\t|\n", string.Empty);
            response = Regex.Replace(response, @"&quot;", string.Empty);
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(response, " ");
        }

        public static string ReplaceWhiteSpaceToPlusSymbol(this string str)
        {
            return str.Replace(" ", StringResources.PlusSymbol);
        }
    }
}
