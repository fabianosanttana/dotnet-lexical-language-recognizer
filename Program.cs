using System;
using AFD.Utils;

namespace AFD
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "./Arquivos/Texto_a_ser_reconhecido.txt";

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Exceptions.Handler);

            FileIterator iterator = new FileIterator(fileName);
            Automato automato = new Automato(iterator);

            TokenLexeme tl = null;
            while (iterator.hasNextToken())
            {
                tl = automato.recuperarProximoTokenLexema();
                
                Console.WriteLine(tl.ToString());
                Log.LogMessage(tl.ToString());
            }
            Console.WriteLine("Foi salvo um arquivo de log na sua área de trabalho com todas as palavras");
            Console.ReadKey();
        }
    }
}
