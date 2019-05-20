using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SymCrypto3
{
    class Program
    {
        static void Main(string[] args)
        {

            
            string text = File.ReadAllText("14.txt", Encoding.GetEncoding(1251));
            Word T = new Word(text);

            Word.arr res;                     


            res = Word.Frequency(T);
            //for (int i = 0; i < res.val.Length; i++)
                //Console.WriteLine( Word.ReWord(res.idx[i]) + "   " + res.val[i]);

            Word.Key_Search(res.idx, T);

            /*
            Word R = new Word("ãüãþäéäùåûåüæéæôæõæöæøæùæûæÿ");
            R.Print();
            */


            Console.WriteLine("end");
            Console.ReadKey();

        }
    }
}
