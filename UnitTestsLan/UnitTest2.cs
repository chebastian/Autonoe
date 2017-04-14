using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XLang;
using System.Collections.Generic;

namespace UnitTestsLan
{
    [TestClass]
    public class ListLexerTest
    {
        [TestMethod]
        public void CanTokenizeListBegin()
        {
            ListLexer lex = new ListLexer("[a,tes,2]");
            lex.consume();
            var token = lex.NextToken();
            Assert.IsTrue(token.AreSame(Token.ListStart), "Expected List start token");
        }

        [TestMethod]
        public void CanTokenizeListEnd()
        {
            ListLexer lex = new ListLexer("]a,tes,2]");
            lex.consume();
            var token = lex.NextToken();
            Assert.IsTrue(token.AreSame(Token.ListEnd), "Expected List start token");
        }

        [TestMethod]
        public void CanTokenizeListDelim()
        {
            ListLexer lex = new ListLexer(",b");
            lex.consume();
            var token = lex.NextToken();
            Assert.IsTrue(token.AreSame(Token.ListDelim), "Expected list delimiter");
        }

        [TestMethod]
        public void CanTokenizeSequence()
        {
            ListLexer lex = new ListLexer("[a,ba,132]");


            var theToken = Token.EOF;
            var tokens = new List<Token>();
            lex.consume();
            do
            {
                theToken = lex.NextToken();
                tokens.Add(theToken);
                //lex.consume(); 
            }
            while (!theToken.AreSame(Token.EOF));

            var expected = new List<Token>() {
                Token.ListStart,
                Token.NamedType, Token.ListDelim,
                Token.NamedType, Token.ListDelim,
                Token.NamedType, Token.ListEnd,
                Token.EOF};


            Assert.AreEqual(expected.Count, tokens.Count, "Amount of tokens differ");
        }
    }
}
