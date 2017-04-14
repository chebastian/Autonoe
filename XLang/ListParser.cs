using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLang
{
    public class ListParser : Parser
    {
        public ListParser(ListLexer lexer)
            :base()
        {
            _lexer = lexer;
            consume();
        } 
    }
}
