using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLang
{
    public abstract class RDLexer
    {
        private string _text;
        private char[] _chars;

        public RDLexer(String text)
        {
            _text = text;
            _chars = _text.ToCharArray();
            ReadingIndex = -1;
        }

        public int ReadingIndex { get; private set; }
        public char Current { get; private set; }

        public static char EOF = '#';

        public char consume() 
        {
            ReadingIndex++;
            if (ReadingIndex >= _chars.Length)
            {
                Current = EOF;
                return EOF; 
            }

            var c = _chars[ReadingIndex];
            Current = _chars[ReadingIndex];

            return c;
        }

        public bool Match(char c)
        {
            return Current.Equals(c);
        }

        public abstract Token NextToken();
        public abstract String TokenName();
    }

    public class NullLexer : RDLexer
    {
        public NullLexer(string text) : base(text)
        {
        }

        public override Token NextToken()
        {
            throw new NotImplementedException();
        }

        public override string TokenName()
        {
            throw new NotImplementedException();
        }
    }
}
