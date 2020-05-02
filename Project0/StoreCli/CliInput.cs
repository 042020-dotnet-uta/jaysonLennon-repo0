using System;
using System.Text.RegularExpressions;

namespace StoreCli
{
    public static class MenuHelper
    {
        public static void AbortThenExit(this CliMenu menu, string message)
        {
            Console.Write($"\n\n{message}\n\nPress any key to continue.");
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

        public static void Error(string message)
        {
            Console.WriteLine($"{message}\n");
        }
    }
    public class CliInput
    {
        public static bool EmailValidator(string value)
        {
            var re = new Regex(@".+@.+\..+");
            if (!re.IsMatch(value))
            {
                CliPrinter.Error("Please enter a valid email address.");
                return false;
            }
            return true;
        }

        public static bool NameValidator(string value)
        {
            foreach(char c in value.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '.' || c == ' ') continue;
                    else {
                        CliPrinter.Error("Only letters are allowed in a name.");
                        return false;
                    }
                }
            }
            return true;
        }

        public static string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter) return password;
                else if (cki.Key == ConsoleKey.Backspace) {
                    if (password.Length > 0) {
                        password = password.Substring(0, password.Length - 1);
                    } else {
                        password = "";
                    }
                } else {
                    if (!Char.IsControl(cki.KeyChar)) password += cki.KeyChar;
                }
            } while (true);
        }

        [Flags]
        public enum GetLineOptions
        {
            TrimSpaces = 1,
            AllowEmpty = 2,
            AbortOnEmpty = 4,
            Required = 8,
        }

        public static string GetLine(GetLineOptions options, Func<string, bool> validator, string prompt)
        {
            string input = "";
            do
            {
                Console.Write($"{prompt} ");

                input = Console.ReadLine();

                if ((options & GetLineOptions.TrimSpaces) != 0) input = input.Trim();

                if ((options & GetLineOptions.AbortOnEmpty) != 0 && input == "") return null;

                if ((options & GetLineOptions.AllowEmpty) != 0 && input == "") break;

                if ((options & GetLineOptions.Required) != 0 && input == "")
                {
                    CliPrinter.Error("Empty value not allowed.");
                    continue;
                }

                if (!validator(input)) continue;
                else break;

            } while (true);

            return input;
        }

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