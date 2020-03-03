using System;
using System.Collections.Generic;
using System.Text;

namespace CaesarAffine
{
    public class Caesar
    {
        public static string caesarEncrypt(int key, string input)
        {
            //E(k,x)=x+k (mod 26)
            return encrypt(1, key, input);
        }
        public static string caesarDecrypt(int key, string input)
        {
            //D(k,y)=y-k (mod 26)
            return decrypt(1, key, input);
        }



        public static string brutalCaesarDecrypt(string input)
        {
            string output = "";
            for (int i = 0; i < 26; i++)
            {
                output += caesarDecrypt(i, input);
                output += '\n';
            }
            return output;
        }


        public static string[] helpCaesarDecrypt(string input, string inputHelper)
        {
            //returns array of strings size 2, index 0 = decrypted cipher, index 1 = guessed key
            if (inputHelper.Length < 1)
            {
                Console.WriteLine("Za mało danych!");
                return null;
            }
            string[] output = new string[2];
            int key;
            for (int i = 0; i < inputHelper.Length; i++)
            {
                if (checkCharAlphabetic(input[i]))
                {
                    key = Convert.ToInt32((input[i] - inputHelper[i]) % 26);
                    output[0] = caesarDecrypt(key, input);
                    if (key < 0)
                        key += ALPH;
                    output[1] = key.ToString();
                    break;
                }
            }
            return output;
        }
    }
}
