using System;
using System.Text;
using System.Text.RegularExpressions;
using XLang;

namespace XLang
{
    public class ListLexer : RDLexer
    { 
        public ListLexer(string text) : base(text)
        { 
        }

        public override Token NextToken()
        {
            var theToken = Token.EOF;
            while(Current != EOF)
            { 
                switch (Current)
                {
                    case ' ':
                        consume();
                        continue;
                    case ',':
                        theToken = Token.ListDelim;
                        consume();
                        break;
                    case '[':
                        theToken = Token.ListStart;
                        consume();
                        break;
                    case ']':
                        theToken = Token.ListEnd;
                        consume();
                        break;
                    default:
                        theToken = Name();
                        break;
                } 

                return theToken;
            }

            return Token.EOF;
        }

        private Token Name()
        {
            var str = new StringBuilder();

            while (CurrentIsLetter())
            {
                str.Append(Current);
                consume();
            }

            return Token.Named(str.ToString());
        }

        private bool CurrentIsLetter()
        {
            var letters = Regex.Match(Current.ToString(), "[a-zA-Z0-9]");
            return letters.Success;
            //return letters;

            //return Current >= 48 && Current < 122;
        }

        public override string TokenName()
        {
            throw new NotImplementedException();
        }
    }
}