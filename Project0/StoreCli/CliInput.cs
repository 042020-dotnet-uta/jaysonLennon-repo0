
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreCli
{
    public class CliPrinter
    {
        public static void Title(string title)
        {
            Console.WriteLine($"=== {title} ===");
        }
    }
    public class CliInput
    {
        public static int? GetInt(string input)
        {
            try
            {
                return Int32.Parse(input);
            }
            catch
            {
                return null;
            }
        }

        public static double? GetDouble(string input)
        {
            try
            {
                return Double.Parse(input);
            }
            catch
            {
                return null;
            }
        }
    }
}