using CurlyCode.Common.Classes;
namespace CurlyCode.Parser
{
    public class TokenList : List<Token>, IList<Token>
    {
        private int TokenPlace; 
        public TokenList() { }

        internal Token? Peek(int count = 0)
        {
            if (count < 0)
                throw new ArgumentException("You cant peek negative places");

            if (TokenPlace>= Count )
                return null;

            return this[TokenPlace + count];
        }

        internal Token? ConsumeToken()
        {
            TokenPlace++;
            return this[TokenPlace];
        }
    }
}
