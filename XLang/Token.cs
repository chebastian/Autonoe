using System;

namespace XLang
{
    public class Token : IComparable<Token> 
    {
        private string v;
        private int type;

        private Token(string v, int t)
        {
            this.v = v;
        }

        public static Token ListDelim { get => new Token("<DELIM>", 0); } 
        public static Token EOF { get => new Token("<EOF>", 1); }
        public static Token ListStart { get => new Token("<L_START>",2); }
        public static Token ListEnd { get => new Token("<L_END>",3); }
        public static Token NamedType { get => Named(String.Empty); }

        public static Token Named(string name)
        {
            return new Token("<NAME: " + name + ">",4);
        }

        public bool AreSame(Token t)
        {
            return t.v.Equals(v);
        }

        public override String ToString() 
        {
            return v;
        }

        public bool Equals(Token t)
        {
            return t.type.Equals(this.type);
        }

        public int CompareTo(Token other)
        {
            return this.type - other.type;
        }

        public Int32 GetHashCode()
        {
            return type.GetHashCode();
        }

    }
}