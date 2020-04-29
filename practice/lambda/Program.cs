using System;
using System.Linq;

namespace lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            string value = "hello";
            var scream = value.ToCharArray().Select(c => Char.ToUpper(c));
            string screaming = string.Join("", scream);
            Console.WriteLine(screaming);
        }
    }
}
