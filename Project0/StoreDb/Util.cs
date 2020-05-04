using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Util
{
    public static class Config
    {
        public const int MAX_ORDER_QUANTITY = 200;
    }
    public static class Hash
    {
        public static String Sha256(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = input.ToCharArray().Select(c => (byte)c).ToArray();
                var hash = sha256.ComputeHash(bytes);
                var encoded = Convert.ToBase64String(hash);
                return encoded;
            }
        }
    }
}