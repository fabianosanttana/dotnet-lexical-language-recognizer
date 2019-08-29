using System;
using System.Collections.Generic;
using System.IO;

namespace AFD
{
    internal class FileIterator
    {
        private int actualIndex;
        private char[] list;

        public FileIterator(string fileName)
        {
            this.actualIndex = -1;

            if (!File.Exists(fileName)) throw new FileNotFoundException("Arquivo específicado não encontrado");

            using (StreamReader file = new StreamReader(fileName))
            {
                list = file.ReadToEnd().ToCharArray();
            }
        }
        public char nextChar()
        {
            if (hasNextToken())
            {
                this.actualIndex++;
                return list[actualIndex];
            }
            return '¨';
        }
        public bool hasNextToken() => (actualIndex + 1) < list.Length;
        public void voltar1Posicao() => this.actualIndex--;
    }
}