using AFD.Enums;
using AFD.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFD
{
    public static class CriarTransicaoDinamica
    {
        public static void criarTransicaoPalavra(this Dictionary<StateValueObject, dynamic> tabelaSimbolos, StateValueObject s0, string lexema, string token)
        {
            StateValueObject last = s0;
            for (int i = 0; i < lexema.Length - 1; i++)
            {
                var dyn = criarEstadoTransicaoUnica((i + 1), last, lexema[i], false);
                last = dyn;
            }


            StateValueObject estadoFinal = new StateValueObject(true, lexema.Length);
            last.addTransition(' ', estadoFinal);
            last.addTransition('\t', estadoFinal);
            last.addTransition('\r', estadoFinal);
            tabelaSimbolos.Add(estadoFinal, token.ToUpper());
        }

        private static StateValueObject criarEstadoTransicaoUnica(int identificador,
        StateValueObject estadoAnterior, char caracterTransicao,
        bool marcado)
        {
            StateValueObject s = new StateValueObject(marcado, identificador);
            if (!estadoAnterior.existsTransition(caracterTransicao))
                estadoAnterior.addTransition(caracterTransicao, s);
            return s;
        }
    }
}
