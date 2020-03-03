using System;
using System.Collections.Generic;
using System.Text;

namespace CaesarAffine
{
    class CryptographyUtils
    {
        public const int AlphabetLength = 26;

        public static string encrypt(int a, int b, string input)
        {
            string output = "";
            char[] chars = input.ToCharArray();
            int x;
            for (int i = 0; i < input.Length; i++)
            {
                if (chars[i] >= 'A' && chars[i] <= 'Z')
                {
                    x = Convert.ToInt32(chars[i] - 'A');
                    output += Convert.ToChar((((x * a) + b + AlphabetLength) % AlphabetLength) + 'A');
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {
                    x = Convert.ToInt32(chars[i] - 'a');
                    output += Convert.ToChar((((x * a) + b + AlphabetLength) % AlphabetLength) + 'a');
                }
                else
                {
                    output += chars[i];
                }
            }
            return output;
        }

        public static string decrypt(int a, int b, string input)
        {
            string output = "";
            char[] chars = input.ToCharArray();
            int x;
            for (int i = 0; i < input.Length; i++)
            {
                if (chars[i] >= 'A' && chars[i] <= 'Z')
                {
                    x = Convert.ToInt32(chars[i] - 'A');
                    if (x - b < 0) x = Convert.ToInt32(x) + AlphabetLength;
                    output += Convert.ToChar(((a * (x - b)) % AlphabetLength) + 'A');
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {

                    x = Convert.ToInt32(chars[i] - 'a');
                    if (x - b < 0) x = Convert.ToInt32(x) + AlphabetLength;
                    output += Convert.ToChar(((a * (x - b)) % AlphabetLength) + 'a');
                }
                else
                {
                    output += chars[i];
                }
            }
            return output;
        }




        public static bool checkCharAlphabetic(char c)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                return true;
            }
            return false;
        }

        public static bool checkCharAlphabeticLower(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return true;
            }
            return false;
        }

        public static bool checkCharAlphabeticUpper(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            return false;
        }

        public static int oppositeModulo(int n, int m)
            //naive method
        {
            for (int i = 1; i < m; i++)
            {
                if ((n * i) % m == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static int gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }
    }
}
