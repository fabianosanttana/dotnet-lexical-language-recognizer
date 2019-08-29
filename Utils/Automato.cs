using AFD.Enums;
using AFD.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace AFD
{
    internal class Automato
    {
        private StateValueObject s0;
        private FileIterator iteradorArquivo;
        private Dictionary<StateValueObject, dynamic> tabelaSimbolos;

        public Automato(FileIterator iteradorArquivo)
        {
            this.iteradorArquivo = iteradorArquivo;
            this.tabelaSimbolos = new Dictionary<StateValueObject, dynamic>();

            s0 = new StateValueObject(false, 0);

            s0.addTransition(' ', s0);
            s0.addTransition('\t', s0);
            s0.addTransition('\n', s0);
            s0.addTransition('\r', s0);

            if (!File.Exists("Arquivos/Dicionario_lexemas.txt")) throw new FileNotFoundException("Arquivo específicado não encontrado");

            var lines = File.ReadLines("Arquivos/Dicionario_lexemas.txt");

            foreach (var item in lines)
            {
                var split = item.Split("|&!");

                tabelaSimbolos.criarTransicaoPalavra(s0, split[0], split[1]);
            }
            tabelaSimbolos.criarTransicaoNumerica(s0);
        }

        public TokenLexeme recuperarProximoTokenLexema()
        {
            TokenLexeme tokenLexema;
            string lexema = "";

            StateValueObject estadoAtual = s0;
            char c = ' ';
            bool podeSerNumerico = false, eNumerico = false, podeSerString = false, eString = false;
            while (
                //Enquanto for token   
                (eNumerico && (c = iteradorArquivo.nextChar()) != '¨' && !IsSpace(c)) ||
                //Enquanto for lexema
                (estadoAtual != null && !estadoAtual.isChecked() && (c = iteradorArquivo.nextChar()) != '¨') ||
                //Enquanto for string
                (eString && estadoAtual is null && (c = iteradorArquivo.nextChar()) != '¨' && !IsSpace(c)) ||
                //Palavra completa que não é string, nem lexema nem inteiro
                (eNumerico is false && eString is false && estadoAtual is null && (c = iteradorArquivo.nextChar()) != '¨' && !IsSpace(c))
                )
            {
                // Validação se o primeiro caracter é uma letra minuscula
                if ((int)c > 96 && (int)c < 123 && s0.Equals(estadoAtual))
                    eString = true;
                
                try
                {
                    if (!eNumerico)
                        estadoAtual = estadoAtual.getNextState(c);
                }
                catch (Exception x)
                {
                    //ignored
                }

                if (eString is false && estadoAtual == null && char.IsNumber(c))
                    eNumerico = true;

                else if (eNumerico && (c.Equals('.') || c.Equals(',')) && (!podeSerNumerico || ((podeSerString = true))))
                {
                    if (eNumerico)
                        podeSerNumerico = true;
                    else
                        podeSerNumerico = false;
                    lexema = lexema + c;
                    continue;
                }

                else if (eNumerico)
                    podeSerString = true;


                if (!s0.Equals(estadoAtual))
                    lexema = lexema + c;
            }
            if(eString && estadoAtual == null)
                tokenLexema = new TokenLexeme("STRING", lexema);
            else if (!podeSerString && (eNumerico || podeSerNumerico))
                tokenLexema = new TokenLexeme("NUMÉRICO", lexema);

            else if (estadoAtual == null)
                tokenLexema = new TokenLexeme(TipoToken.NAO_RECONHECIDA, lexema);

            else
            {
                if (estadoAtual.isChecked())
                {
                    string tipoToken = tabelaSimbolos[estadoAtual];
                    tokenLexema = new TokenLexeme(tipoToken, lexema);
                }
                else if (s0.Equals(estadoAtual))
                    tokenLexema = new TokenLexeme(TipoToken.DESCARTAR, "");
                else
                    tokenLexema = new TokenLexeme(TipoToken.NAO_RECONHECIDA, lexema);
            }

            return tokenLexema;
        }

        private bool IsSpace(char c)
        {
            return c.Equals(' ') || c.Equals('\t') || c.Equals('\r');
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