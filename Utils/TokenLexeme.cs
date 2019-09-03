namespace AFD
{
    internal class TokenLexeme
    {
        private object descart;
        private string v;

        /// <summary>
        /// Método construtor para lexema
        /// </summary>
        /// <param name="descart">Token</param>
        /// <param name="v">Palavra do arquivo</param>
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