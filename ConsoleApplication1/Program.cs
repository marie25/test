using System;
using System.IO;

namespace ConsoleApplication1
{

    class invalidNumberException : Exception
    {
        public invalidNumberException(string message) : base(message) { }
    }

    class Program
    {
        readonly static int I = 1;
        readonly static int V = 5;
        readonly static int X = 10;
        readonly static int L = 50;
        readonly static int C = 100;
        readonly static int D = 500;
        readonly static int M = 1000;

        readonly static int[] arabicNums = new int[13] {1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000};
        readonly static string[] romanNums = new string[13] {"I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M"};

        static int toArabicNumeral(char roman)
        {
            switch (roman)
            {
                case 'I':
                    return I;
                case 'V':
                    return V;
                case 'X':
                    return X;
                case 'L':
                    return L;
                case 'C':
                    return C;
                case 'D':
                    return D;
                case 'M':
                    return M;
            }
            throw new invalidNumberException(roman + " is not a roman numeral.");
        }

        static int toArabic(string roman)
        {
            if (roman.Length == 0)
            {
                return 0;
            }
            int sum = 0;
            bool lastLessThanCur = false;
            int last = toArabicNumeral(roman[0]);
            sum += last;
            for (int i = 1; i < roman.Length; i++)
            {
                int cur = toArabicNumeral(roman[i]);
                if (last < cur)
                {
                    if (lastLessThanCur)
                    {
                        throw new invalidNumberException("This number doesn't match unique numeral");
                    }
                    lastLessThanCur = true;
                    sum -= 2 * last;
                } else
                {
                    lastLessThanCur = false;
                }
                sum += cur;
                last = cur;
            }
            return sum;
        }

        static string toRoman(int arabic)
        {
            string ans = "";
            for (int i = 12; i >= 0; i--)
            {
                while (arabic >= arabicNums[i])
                {
                    arabic -= arabicNums[i];
                    ans += romanNums[i];
                }
            }
            return ans;
        }

        static void Main(string[] args)
        {
            try
            {
                StreamWriter outputFile = new StreamWriter(@"C:\Users\mbezhashvili\Desktop\romans.out.txt", false);
                foreach (string inputRomanNumber in File.ReadAllLines(@"C:\Users\mbezhashvili\Desktop\romans.in.txt"))
                {
                    int arabicNumber = toArabic(inputRomanNumber);
                    string outputRomanNumber = toRoman(arabicNumber);
                    outputFile.WriteLine(outputRomanNumber);
                }
                outputFile.Close();
            }
            catch (invalidNumberException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
