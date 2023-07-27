using System.Security.Cryptography;
using System.Text;

namespace SharedHelpers.HelperServices
{
    public static class HashingService
    {
        public static string GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        // Function to Generate a random string with a given size
        public static string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;
            for (var i = 0; i < size; i++)
            {
                var @char = (char)RandomNumberGenerator.GetInt32(offset, offset + lettersOffset);
                builder.Append(@char);
            }
            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
