using System.Text;

namespace api.Utils
{
    public class Base62
    {
        private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
        public static string Encode(ulong number)
        {
            if(number == 0) return Alphabet[0].ToString();
        
            StringBuilder builder = new StringBuilder();

            while (number > 0)
            {
                ulong remainder = number % 62;
                builder.Insert(0, Alphabet[(int) remainder]);
                number /= 62;
            }

            return builder.ToString();
        }
    }
}