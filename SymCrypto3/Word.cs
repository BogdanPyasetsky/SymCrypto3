using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SymCrypto3
{
    class Word
    {
        private static int a_size = 31; // alphabeth size
        private static int m2 = a_size * a_size;
        private int[] word;
        public struct arr
        {
            public float[] val;
            public int[] idx;
        }



        private int Convert(char lett)
        {
            char[] symbol = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о',
                                    'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ы', 'ь', 'э', 'ю', 'я' };
            for (int i = 0; i < a_size; i++)
                if (lett == symbol[i])
                    return i;
            return -1;
        }

        public static char ReConvert(int x)
        {
            char[] symbol = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о',
                                    'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ы', 'ь', 'э', 'ю', 'я' };
            for (int i = 0; i < a_size; i++)
                if (x == i)
                    return symbol[i];
            return '?';
        }



        public void Print()
        {
            for (int i = 0; i < word.Length; i++)
            {
                Console.Write(word[i] + "  ");
            }
            Console.WriteLine();
        }



        public Word (int x)
        {
            word = new int[x];
        }

        public Word(string str)
        {
            int word_len = str.Length / 2;
            word = new int[word_len];
            for (int i = 0; i < word_len; i++)
            {
                word[i] = a_size * Convert(str[2 * i]) + Convert(str[2 * i + 1]);
            }
        }

        public static string ReWord(int[] x)
        {
            char a1, a2;
            string res = "";
            int temp1, temp2;
            for (int i = 0; i < x.Length; i++)
            {
                temp1 = x[i]; temp2 = x[i];
                while (temp1 > 30)
                    temp1 -= 31;
                a2 = ReConvert(temp1);
                temp2 -= temp1;
                a1 = ReConvert(temp2 / 31);
                res += a1;
                res += a2;
            }
            //Console.WriteLine(res);
            return  res;
        }



        private static int Mod(int x)
        {
            while (x >= m2)
                x -= m2;
            while (x < 0)
                x += m2;
            return x;
        }

        private static int Mod(int x, int n)
        {
            while (x >= n)
                x -= n;
            while (x < 0)
                x += n;
            return x;
        }



        private static int GCD(int a, int b, out int u, out int v)
        {
            if (a == 0)
            {
                u = 0;
                v = 1;
                return b;
            }
            else
            {
                int u1, v1;
                int d = GCD(b % a, a, out u1, out v1);
                u = v1 - (b / a) * u1;
                v = u1;
                return d;
            }
        }



        private static int[] LinComp(int Y, int a, int n)
        {
            int u, v;
            int d = GCD(a, n, out u, out v);                               //GCD(a, m2, out u, out v);
            if (d == 1)
            {
                int[] res = new int[1];
                res[0] = Mod(Y * u, n);                                    // Mod(Y * u);
                return res;
            }
            else
            {
                if(Y % d == 0)
                {
                    int a1 = a / d;
                    int Y1 = Y / d;
                    int n1 = n / d;                                        //int n1 = m2 / d;
                    int[] res = new int[d];
                    var d1 = GCD(a1, n1, out u, out v);
                    res[0] = Mod(Y1 * u, n1);
                    for (int i = 1; i < d; i++)
                    {
                        res[i] = Mod(res[0] + i * n1, n);                  //Mod(res[0] + i * n1);
                    }
                    return res;
                }
                else
                {
                    int[] res = new int[] {-999};
                    return res;
                }
                
            }        
        }
                                 

        
        public static arr Frequency(Word X)
        {

            arr res = new arr();
            res.val = new float[m2];
            res.idx = new int[m2];
            
            for (int i = 0; i < X.word.Length; i++)
                res.val[X.word[i]]++;
            for (int i = 0; i < m2; i++)
            {
                res.val[i] = res.val[i] / X.word.Length;
                res.idx[i] = i;
            }

            for (int i = 1; i < res.val.Length; i++)
            {
                float cur = res.val[i];
                int cur_idx = res.idx[i];
                int j = i;
                while (j > 0 && cur < res.val[j - 1])
                {
                    res.val[j] = res.val[j - 1];
                    res.idx[j] = res.idx[j - 1];
                    j--;
                }
                res.val[j] = cur;
                res.idx[j] = cur_idx;
            }

            Array.Reverse(res.val);
            Array.Reverse(res.idx);

            arr shortres = new arr();
            shortres.val = new float[5];
            shortres.idx = new int[5];
            for (int i = 0; i < 5; i++)
            {
                shortres.val[i] = res.val[i];
                shortres.idx[i] = res.idx[i];
            }

            //return shortres.idx;
            return shortres;
        }



        private static bool Filter(int[] d_word)
        {
            int[] corr = new int[] { 26, 27, 40, 51, 53, 60, 91, 102, 118, 119, 120, 122, 133, 149, 181, 182, 195, 206, 207, 208, 210, 211, 212, 216 };
            for (int w_idx = 0; w_idx < d_word.Length; w_idx++)
            {
                for (int c_idx = 0; c_idx < corr.Length; c_idx++)
                {
                    if (d_word[w_idx] == corr[c_idx])
                    {
                        
                        return false;
                        
                    }
                    
                }

                
            }
            return true;
        }



        public static int[] Decrypt(Word Y, int a, int b, int n)
        {
            int[] x_part;
            int[] res = new int[Y.word.Length];
            for (int i = 0; i < Y.word.Length; i++)
            {
                x_part = LinComp(Y.word[i] - b, a, n);
                res[i] = x_part[0];
            }
            return res;
        }



        public static void Key_Search(int[] Y, Word W)
        {
            File.Delete("DecryptedText.txt");
            bool filt;
            int[] X = new int[] { 545, 417, 572, 403, 168 };
            int X1, X2, Y1, Y2;
            int[] a;
            int[] d_word;
            int b;
            string res;
            for (int t_Y1 = 0; t_Y1 < 5; t_Y1 ++)
            {
                Y1 = Y[t_Y1];

                for (int t_Y2 = 0; t_Y2 < 5; t_Y2++)
                {
                    if (t_Y1 == t_Y2)
                        if (t_Y1 != 4)
                            Y2 = Y[t_Y2 + 1];
                        else
                            break;
                    else
                        Y2 = Y[t_Y2];

                    for (int t_X1 = 0; t_X1 < 5; t_X1++)
                    {
                        X1 = X[t_X1];

                        for (int t_X2 = 0; t_X2 < 5; t_X2++)
                        {

                            if (t_X1 == t_X2)
                                if (t_X1 != 4)
                                    X2 = X[t_X2 + 1];
                                else
                                    break;
                            else
                                X2 = X[t_X2];

                            //Console.WriteLine(Y1 + "  " + Y2 + "  " + X1 + "  " + X2);


                            
                            a = LinComp(Mod(Y1 - Y2), Mod(X1 - X2), m2);
                            for (int i = 0; i < a.Length; i++)
                            {
                                b = Mod(Y1 - a[i] * X1);
                                if (a[i] != -999)
                                {
                                    d_word = Decrypt(W, a[i], b, m2);
                                    filt = Filter(d_word);
                                    if (filt == true)
                                    {
                                        res = ReWord(d_word);

                                        File.AppendAllText("DecryptedText.txt", res);
                                        File.AppendAllText("DecryptedText.txt", "           ");
                                        Console.WriteLine("a: " + a[i] + "    b: " + b);
                                    }


                                }
                                
                            }
                            //Console.WriteLine("---------------------------------------------------");

                        }
                    }
                }
            }


        }
    }
}
