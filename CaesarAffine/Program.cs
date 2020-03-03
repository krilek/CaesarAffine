using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CaesarAffine
{

    class Program
    {
        public static string filePlain = "plain.txt";
        public static string fileKey = "key.txt";
        public static string fileCrypto = "crypto.txt";
        public static string fileDecrypt = "decrypt.txt";
        public static string fileExtra = "extra.txt";
        public static string fileKeyNew = "key-new.txt";

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Za mało argumentów!");
                return;
            }

            try
            {
                if (args[0].Equals("-c"))
                {
                    if (args[1].Equals("-e")) //szyfrowanie
                    {
                        string plain = readFileLine(filePlain);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string cipher = Caesar.caesarEncrypt(key[0], plain);
                        File.WriteAllText(fileCrypto, cipher);

                    }
                    else if (args[1] == "-d") //odszyfrowanie
                    {
                        string cipher = readFileLine(fileCrypto);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string plain = Caesar.caesarDecrypt(key[0], cipher);
                        File.WriteAllText(fileDecrypt, plain);
                    }
                    else if (args[1] == "-j") //kryptoanaliza z tekstem jawnym
                    {
                        string cipher = readFileLine(fileCrypto);
                        string helper = readFileLine(fileExtra);
                        string[] krypto = Caesar.helpCaesarDecrypt(cipher, helper);
                        File.WriteAllText(fileKeyNew, krypto[1]);
                        File.WriteAllText(fileDecrypt, krypto[0]);
                    }
                    else if (args[1] == "-k") //kryptoanaliza wyłącznie w oparciu o kryptogram
                    {
                        string cipher = readFileLine(fileCrypto);
                        string krypto = Caesar.brutalCaesarDecrypt(cipher);
                        File.WriteAllText(fileDecrypt, krypto);
                    }
                }
                else if (args[0] == "-a")
                {
                    if (args[1] == "-e")
                    {
                        string plain = readFileLine(filePlain);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string cipher = Affine.affineEncrypt(key[0], key[1], plain);
                        File.WriteAllText(fileCrypto, cipher);
                    }
                    else if (args[1] == "-d")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string plain = Affine.affineDecrypt(key[0], key[1], cipher);
                        File.WriteAllText(fileDecrypt, plain);
                    }
                    else if (args[1] == "-j")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string helper = readFileLine(fileExtra);
                        string[] krypto = Affine.helpAffineDecrypt(cipher, helper, 0);
                        File.WriteAllText(fileKeyNew, krypto[1] + " " + krypto[2]);
                        File.WriteAllText(fileDecrypt, krypto[0]);
                    }
                    else if (args[1] == "-k")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string krypto = Affine.brutalAffineDecrypt(cipher);
                        File.WriteAllText(fileDecrypt, krypto);
                    }
                }
                else
                {
                    Console.WriteLine("Użyto złych argumentów! Uzyj np. nazwa.exe -a -d");
                }
            }
            catch (Exception) { Console.WriteLine("Błąd!"); }
        }



        //FUNCKJE
        





        public static string readFileLine(string filename)
        {
            string output = File.ReadLines(filename).First();
            return output;
        }

        public static int[] retrieveKeys(string input)
        {
            string[] outputH = new string[2];
            int[] output = new int[2];
            char[] t = input.ToCharArray();
            int secondNumber = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (t[i] == 32)
                {
                    secondNumber = 1;
                }
                if (secondNumber == 0)
                {
                    outputH[0] += t[i];
                }
                else if (secondNumber == 1)
                {
                    outputH[1] += t[i];
                }
            }
            output[0] = Int32.Parse(outputH[0]);
            output[1] = Int32.Parse(outputH[1]);

            return output;
        }
    }
}