using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XLang;

namespace UnitTestsLan
{
    [TestClass]
    public class ParserTest
    {
        private ListParser _parser;

        [TestInitialize]
        public void setup()
        {
            var list = "[123,abc,dd]";
            _parser = new ListParser(new ListLexer(list));
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(_parser.match(Token.ListStart), "No match");
        }
    }
}
