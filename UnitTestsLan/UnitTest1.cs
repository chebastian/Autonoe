using Microsoft.VisualStudio.TestTools.UnitTesting;
using XLang;

namespace UnitTestsLan
{
    [TestClass]
    public class RDLexerTests 
    {

        string text;
        RDLexer lexer;

        [TestInitialize]
        public void setup()
        {
             text = "[one,two,3,fyra]";
            lexer = new NullLexer(text);
        }

        [TestMethod]
        public void ShouldCreateLexerWithText()
        {
            var c = lexer.consume();
            var expectedFirstchar = '['; 
            Assert.AreEqual(c,expectedFirstchar,"Unexpected first token");
        }

        [TestMethod]
        public void SholdCreateTokens()
        {
            lexer.consume();
            Assert.IsTrue(lexer.Match('[')); 
        }
    }
}
