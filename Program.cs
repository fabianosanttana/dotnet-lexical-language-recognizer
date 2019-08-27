using System;
using AFD.Utils;

namespace AFD
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = "./file.txt";

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Exceptions.Handler);

            FileIterator iterator = new FileIterator(fileName);
            Automato automato = new Automato(iterator);

            TokenLexeme tl = null;
            while (iterator.hasNextToken())
            {
                tl = automato.recuperarProximoTokenLexema();
                Console.WriteLine(tl.ToString());
            }
            Console.ReadKey();
        }
    }
}
