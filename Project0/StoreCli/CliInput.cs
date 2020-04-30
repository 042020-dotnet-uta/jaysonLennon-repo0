
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreCli
{
    public static class MenuHelper
    {
        public static void AbortThenExit(this CliMenu menu, string message)
        {
            Console.Write($"\n{message}\n\nPress any key to return.");
            Console.ReadKey(true);
            menu.MenuExit();
        }
    }

    public class CliPrinter
    {
        public static void Title(string title)
        {
            Console.WriteLine($"=== {title} ===\n\n");
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