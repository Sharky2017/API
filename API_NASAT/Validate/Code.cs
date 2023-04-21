namespace API_NASAT.Validate
{
    public static class Code
    {
        public static string Generate()
        {

            string result = "";
            string pat = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
            Random Generator = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i <= 5; i++)
            {
                int mIndex = Generator.Next(pat.Length);
                result += pat[mIndex];
            }
            return result;


        }
    }
}
