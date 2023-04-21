namespace API_NASAT.Validate
{
    public static class Chars
    {

        public static bool Symb(this string pass)
        {
            bool count = (
                pass.Split('[', '{', '}', ']', '-', '!', '·', '$', '%', '&', '/', '(', ')', '=', '¿', '¡', '?', ',', '_', ':', ';', ',', '|', '@', '#', '€', '*', '+', '.')
                .Length - 1) == 2;
            return count;
        }

        public static bool Num(this string pass)
        {
            bool count = (
                pass.Split('1', '2', '3', '4', '5', '6', '7', '8', '9', '0')
                .Length - 1) == 2;
            return count;
        }

        public static bool MayusC(this string pass)
        {
            bool count = (
                pass.Split('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z')
                .Length - 1) == 3;
            return count;
        }

        public static bool MinusC(this string pass)
        {
            bool count = (
                pass.Split('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z')
                .Length - 1) == 3;
            return count;
        }
    }
}
