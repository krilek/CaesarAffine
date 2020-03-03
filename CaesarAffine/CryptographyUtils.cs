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
                    x = Convert.ToInt32(chars[i] - 65);
                    output += Convert.ToChar((((x * a) + b + AlphabetLength) % AlphabetLength) + 65);
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {
                    x = Convert.ToInt32(chars[i] - 97);
                    output += Convert.ToChar((((x * a) + b + AlphabetLength) % AlphabetLength) + 97);
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
                    x = Convert.ToInt32(chars[i] - 65);
                    if (x - b < 0) x = Convert.ToInt32(x) + AlphabetLength;
                    output += Convert.ToChar(((a * (x - b)) % AlphabetLength) + 65);
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {

                    x = Convert.ToInt32(chars[i] - 97);
                    if (x - b < 0) x = Convert.ToInt32(x) + AlphabetLength;
                    output += Convert.ToChar(((a * (x - b)) % AlphabetLength) + 97);
                }
                else
                {
                    output += chars[i];
                }
            }
            return output;
        }


        public static bool checkAffineKey(int a, int range)
        {
            if ((gcd(a, range) == 1) && ((a * (oppositeModulo(a, range)) % range) == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static int oppositeModulo(int n)
        //naive method
        {
            for (int i = 1; i < AlphabetLength; i++)
            {
                if ((n * i) % AlphabetLength == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static int gcd(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = a % b;

                a = b;
                b = temp;
            }
            return a;
        }
    }
}
