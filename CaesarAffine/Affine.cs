namespace CaesarAffine
{
    class Affine
    {

        public static string affineEncrypt(int a, int b, string input)
        {
            //E(a,b,x)=a*x+b (mod 26)
            if (gcd(a, ALPH) != 1)
            {
                Console.WriteLine("Niepoprawny klucz do enkrypcji");
                return null;
            }
            return encrypt(a, b, input);
        }

        public static string affineDecrypt(int a, int b, string input)
        {
            //D(a,b,y)=a'*(y-b) (mod 26)
            int aOpposite = CryptographyUtils.oppositeModulo(a, ALPH);
            if (aOpposite == 0)
            {
                Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + ALPH + " dla " + a + "!");
                return null;
            }
            else
                return decrypt(aOpposite, b, input);
        }
    }
}
