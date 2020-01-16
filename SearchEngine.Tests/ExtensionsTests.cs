using SearchEngine.Business.Extensions;
using Xunit;

namespace SearchEngine.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void RemoveWrongSymbolsFromYandexResponse_InsertString_ReturnTrue()
        {
            // arrange
            string expected = "Donald Trump is the 45th and current president of the United States";
            string actual = "<hlword>Donald Trump</hlword> is the 45th &quot;and current    president of the <hlword>United States";

            // act 
            actual = actual.RemoveWrongSymbolsFromYandexResponse();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReplaceWhiteSpaceToPlusSymbol_InsertString_ReturnTrue()
        {
            // arrange
            string expected = "I+want+to+tell+about+by+favorite+series+‘Downton+Abbey’.+This+miracle+of+cinematic+art+is+based+on+the+book+by+English+novelist+Julian+Fellowes.";
            string actual = "I want to tell about by favorite series ‘Downton Abbey’. This miracle of cinematic art is based on the book by English novelist Julian Fellowes.";

            // act
            actual = actual.ReplaceWhiteSpaceToPlusSymbol();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
