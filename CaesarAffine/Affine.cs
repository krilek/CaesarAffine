using System;
using System.Collections.Generic;

namespace CaesarAffine
{
    class Affine
    {

        public static string affineEncrypt(int a, int b, string input)
        {
            //E(a,b,x)=a*x+b (mod 26)
            if (CryptographyUtils.gcd(a, CryptographyUtils.AlphabetLength) != 1)
            {
                Console.WriteLine("Niepoprawny klucz do enkrypcji");
                return null;
            }
            return CryptographyUtils.encrypt(a, b, input);
        }

        public static string affineDecrypt(int a, int b, string input)
        {
            //D(a,b,y)=a'*(y-b) (mod 26)
            int aOpposite = CryptographyUtils.oppositeModulo(a, CryptographyUtils.AlphabetLength);
            if (aOpposite == 0)
            {
                Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + CryptographyUtils.AlphabetLength + " dla " + a + "!");
                return null;
            }
            else
                return CryptographyUtils.decrypt(aOpposite, b, input);
        }

        public static string brutalAffineDecrypt(string input)
        {
            string output = "";
            int a;
            var list = new List<int>();
            for (int i = 1; i < CryptographyUtils.AlphabetLength; i++)
            {
                if (checkAffineKey(i, CryptographyUtils.AlphabetLength))
                {
                    list.Add(i);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                a = list[i];
                int aOpposite = CryptographyUtils.oppositeModulo(a, CryptographyUtils.AlphabetLength);
                if (aOpposite == 0)
                {
                    Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + CryptographyUtils.AlphabetLength + " dla " + a + "!");
                }
                else
                {
                    for (int b = 0; b < CryptographyUtils.AlphabetLength; b++)
                    {
                        output += affineDecrypt(aOpposite, b, input);
                        output += "\n";
                    }
                }
            }
            return output;
        }


        public static string[] helpAffineDecrypt(string input, string inputHelper, int startingIndex)
        {
            //returns array of strings size 3, index 0 = decrypted cipher, index 1 = guessed a, index 2 = guessed b
            //https://www.youtube.com/watch?v=ry3g0xN8QKU
            if (inputHelper.Length < 2)
            {
                Console.WriteLine("Za mało danych aby odgadnac klucze!");
                return null;
            }
            string[] output = new string[3];
            int[,] tab = new int[2, 2];
            int firstPairIndex = 0;
            int firstPairFound = 0;
            int secondPairIndex = 0;
            int secondPairFound = 0;
            for (int i = startingIndex; i < inputHelper.Length; i++)
            {
                if (CryptographyUtils.checkCharAlphabeticLower(inputHelper[i]))
                {
                    tab[0, 0] = Convert.ToInt32(inputHelper[i] - 97);
                    tab[0, 1] = Convert.ToInt32(input[i] - 97);
                    firstPairIndex = i;
                    firstPairFound = 1;
                    break;
                }
                if (CryptographyUtils.checkCharAlphabeticUpper(inputHelper[i]))
                {
                    tab[0, 0] = Convert.ToInt32(inputHelper[i] - 'A');
                    tab[0, 1] = Convert.ToInt32(input[i] - 'A');
                    firstPairIndex = i;
                    firstPairFound = 1;
                    break;
                }
            }

            if (firstPairFound == 1)
            {
                //Console.WriteLine("Znalazłem pierwsza pare");
                firstPairIndex++;
                if (firstPairIndex == inputHelper.Length - 1)
                {
                    Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                    return null;
                }

                for (int i = firstPairIndex; i < inputHelper.Length; i++)
                {
                    if (CryptographyUtils.checkCharAlphabeticLower(inputHelper[i]))
                    {
                        tab[1, 0] = Convert.ToInt32(inputHelper[i] - 'a');
                        tab[1, 1] = Convert.ToInt32(input[i] - 'a');
                        secondPairFound = 1;
                        secondPairIndex = i;
                        break;
                    }
                    if (CryptographyUtils.checkCharAlphabeticUpper(inputHelper[i]))
                    {
                        tab[1, 0] = Convert.ToInt32(inputHelper[i] - 'A');
                        tab[1, 1] = Convert.ToInt32(input[i] - 'A');
                        secondPairFound = 1;
                        secondPairIndex = i;
                        break;
                    }
                }

                if (secondPairFound == 1)
                {
                    Console.WriteLine("Znalazłem dwie pary");
                    Console.WriteLine("extra: " + Convert.ToChar(tab[0, 0] + 97) + " " + Convert.ToChar(tab[1, 0] + 97));
                    Console.WriteLine("cipher: " + Convert.ToChar(tab[0, 1] + 97) + " " + Convert.ToChar(tab[1, 1] + 97));
                    Console.WriteLine("Sprawdzam...");
                    int a, b;
                    int x = Convert.ToInt32(tab[0, 0] - tab[1, 0]);
                    if (x < 0)
                        x += CryptographyUtils.AlphabetLength;
                    int y = Convert.ToInt32(tab[0, 1] - tab[1, 1]);
                    if (y < 0)
                        y += CryptographyUtils.AlphabetLength;
                    int xOpposite = CryptographyUtils.oppositeModulo(x, CryptographyUtils.AlphabetLength);
                    if (xOpposite != 0)
                    {
                        a = (y * xOpposite) % CryptographyUtils.AlphabetLength;
                        b = (tab[0, 1] - ((a * tab[0, 0]) % CryptographyUtils.AlphabetLength) + CryptographyUtils.AlphabetLength) % CryptographyUtils.AlphabetLength;
                        if (a != 0)
                        {
                            output[1] = a.ToString();
                            output[2] = b.ToString();
                            int aOpposite = CryptographyUtils.oppositeModulo(a, CryptographyUtils.AlphabetLength);
                            if (aOpposite == 0)
                            {
                                Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + CryptographyUtils.AlphabetLength + " dla " + a + "!");
                                return null;
                            }
                            else
                            {
                                Console.WriteLine("Udało się! Klucz to : " + a + ", " + b);
                                output[0] = affineDecrypt(a, b, input);
                                return output;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niepoprawne a");
                            return helpAffineDecrypt(input, inputHelper, secondPairIndex);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie mogę znaleźć przeciwności dla " + x + " w mod " + CryptographyUtils.AlphabetLength);
                        if (secondPairIndex == inputHelper.Length - 1)
                            return null;
                        return helpAffineDecrypt(input, inputHelper, secondPairIndex);

                    }
                }
                else
                {
                    Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                    return null;
                }

            }

            else
            {
                Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                return null;
            }

            return null;
        }

        public static bool checkAffineKey(int a, int range)
        {
            if ((CryptographyUtils.gcd(a, range) == 1) && ((a * (CryptographyUtils.oppositeModulo(a, range)) % range) == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
