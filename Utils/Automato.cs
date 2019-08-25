using AFD.Enums;
using AFD.ValueObjects;
using System.Collections.Generic;

namespace AFD
{
    internal class Automato
    {
        private StateValueObject s0;
        private FileIterator iteradorArquivo;
        private Dictionary<StateValueObject, TipoToken> tabelaSimbolos;

        public Automato(FileIterator iteradorArquivo)
        {
            this.iteradorArquivo = iteradorArquivo;
            this.tabelaSimbolos = new Dictionary<StateValueObject, TipoToken>();

            s0 = new StateValueObject(false, 0);

            s0.addTransition(' ', s0);
            s0.addTransition('\t', s0);
            s0.addTransition('\n', s0);
            s0.addTransition('\r', s0);

            criarTransicoesPalavra01();

            criarTransicoesPalavra02();
        }

        public TokenLexeme recuperarProximoTokenLexema()
        {
            TokenLexeme tokenLexema;
            string lexema = "";

            StateValueObject estadoAtual = s0;
            string c = " ";

            while (estadoAtual != null &&
                  !estadoAtual.isChecked() &&
                  (c = iteradorArquivo.nextChar()) != null)
            { 
                estadoAtual = estadoAtual.getNextState(c[0]);
                if (!s0.Equals(estadoAtual))
                    lexema = lexema + c;  
            }

            if (estadoAtual == null)            
                tokenLexema = new TokenLexeme(TipoToken.NAO_RECONHECIDA, lexema);
            
            else
            {
                if (estadoAtual.isChecked())
                {
                    TipoToken tipoToken = tabelaSimbolos[estadoAtual];
                    tokenLexema = new TokenLexeme(tipoToken, lexema);
                }
                else if (s0.Equals(estadoAtual))
                    tokenLexema = new TokenLexeme(TipoToken.DESCARTAR, "");
                else
                    tokenLexema = new TokenLexeme(TipoToken.NAO_RECONHECIDA, lexema);
            }

            return tokenLexema;
        }

        private void criarTransicoesPalavra01()
        {
            StateValueObject s1 = criarEstadoTransicaoUnica(1, s0, 't', false);
            StateValueObject s2 = criarEstadoTransicaoUnica(2, s1, 'a', false);
            StateValueObject s3 = criarEstadoTransicaoUnica(3, s2, 'v', false);
            StateValueObject s4 = criarEstadoTransicaoUnica(4, s3, 'a', false);
            StateValueObject s5 = criarEstadoTransicaoUnica(5, s4, 'r', false);
            StateValueObject s6 = criarEstadoTransicaoUnica(6, s5, 'e', false);
            StateValueObject s7 = criarEstadoTransicaoUnica(7, s6, 's', false);

            StateValueObject s8 = new StateValueObject(true, 8);
            s7.addTransition(' ', s8);
            s7.addTransition('\t', s8);
            s7.addTransition('\r', s8);

            tabelaSimbolos.Add(s8, TipoToken.TAVARES);
        }

        private void criarTransicoesPalavra02()
        {
            StateValueObject s9 = criarEstadoTransicaoUnica(9, s0, 'p', false);
            StateValueObject s10 = criarEstadoTransicaoUnica(10, s9, 'r', false);
            StateValueObject s11 = criarEstadoTransicaoUnica(11, s10, 'o', false);
            StateValueObject s12 = criarEstadoTransicaoUnica(12, s11, 'f', false);
            StateValueObject s13 = criarEstadoTransicaoUnica(13, s12, 'e', false);
            StateValueObject s14 = criarEstadoTransicaoUnica(14, s13, 's', false);
            StateValueObject s15 = criarEstadoTransicaoUnica(15, s14, 's', false);
            StateValueObject s16 = criarEstadoTransicaoUnica(16, s15, 'o', false);
            StateValueObject s17 = criarEstadoTransicaoUnica(17, s16, 'r', false);

            StateValueObject s18 = new StateValueObject(true, 18);
            s17.addTransition(' ', s18);
            s17.addTransition('\t', s18);
            s17.addTransition('\r', s18);

            tabelaSimbolos.Add(s18, TipoToken.PROFESSOR);
        }

        private StateValueObject criarEstadoTransicaoUnica(int identificador,
                StateValueObject estadoAnterior, char caracterTransicao,
                bool marcado)
        {
            StateValueObject s = new StateValueObject(marcado, identificador);
            estadoAnterior.addTransition(caracterTransicao, s);
            return s;
        }
    }
}