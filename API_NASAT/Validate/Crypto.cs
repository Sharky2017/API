namespace API_NASAT.Validate
{
    public static class Crypto
    {
        public static string Encriptar(this string _cadenaAencriptar)
        {
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            string resultado = Convert.ToBase64String(encryted);
            return resultado;
        }
    }
}
