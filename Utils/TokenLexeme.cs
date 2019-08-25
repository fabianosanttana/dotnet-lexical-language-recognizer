namespace AFD
{
    internal class TokenLexeme
    {
        private object descart;
        private string v;

        public TokenLexeme(object descart, string v)
        {
            this.descart = descart;
            this.v = v;
        }

        public override string ToString()
        {
            return $"{ descart } - { v }";
        }
    }
}