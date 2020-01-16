using PublicResXFileCodeGenerator;
using System.Text.RegularExpressions;

namespace SearchEngine.Business.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Removes wrong symbols from yandex response (ex. hlword-tag, tabulation, new line, quot)
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string RemoveWrongSymbolsFromYandexResponse(this string response)
        {
            response = response.Replace(@"<hlword>", string.Empty).Replace(@"</hlword>", string.Empty);
            response = Regex.Replace(response, @"\t|\n", string.Empty);
            response = Regex.Replace(response, @"&quot;", string.Empty);
            // remove white spaces
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(response, " ");
        }

        /// <summary>
        /// Replaces white space to "+"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceWhiteSpaceToPlusSymbol(this string str)
        {
            return str.Replace(" ", StringResources.PlusSymbol);
        }
    }
}
