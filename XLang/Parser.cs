using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLang;

namespace XLang
{
    public class Parser
    {
        protected RDLexer _lexer;
        public Token Lookahead { get; set; }

        public bool match(Token t)
        {
            if (!Lookahead.Equals(t))
                return false;

            consume();

            return true;
        }

        public void consume()
        {
            Lookahead = _lexer.NextToken();
        }
    }
}
