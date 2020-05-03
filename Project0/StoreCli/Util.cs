using System;
using System.Text.RegularExpressions;

namespace Util
{
    public static class MenuHelper
    {
        public static void AbortThenExit(this CliMenu menu, string message)
        {
            CliPrinter.Error(message);
            CliInput.PressAnyKey();
            menu.MenuExit();
        }
    }

    public class CliFormatter
    {
        public static string Title(string title)
        {
            return $"=== {title} ===\n";
        }

        public static string Error(string message)
        {
            return $"\n{message}\n";
        }
    }

    public class CliPrinter
    {
        public static void Title(string title)
        {
            Console.WriteLine(CliFormatter.Title(title));
        }

        public static void Error(string message)
        {
            Console.WriteLine(CliFormatter.Error(message));
        }
    }
    public class CliInput
    {
        public static void PressAnyKey(string message)
        {
            Console.WriteLine(message);
            PressAnyKey();
        }
        public static void PressAnyKey()
        {
            Console.WriteLine("\n\nPress any key to continue.");
            Console.ReadKey();
        }
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
            foreach (char c in value.ToCharArray())
            {
                if (!Char.IsLetter(c))
                {
                    if (c == '.' || c == ' ') continue;
                    else
                    {
                        CliPrinter.Error("Only letters are allowed in a name.");
                        return false;
                    }
                }
            }
            return true;
        }

        public static string GetPassword(string prompt)
        {
            Console.Write($"{prompt} ");
            string password = "";
            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter) return password;
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                    }
                    else
                    {
                        password = "";
                    }
                }
                else
                {
                    if (!Char.IsControl(cki.KeyChar)) password += cki.KeyChar;
                }
            } while (true);
        }

        [Flags]
        public enum GetLineOptions
        {
            TrimSpaces = 1,
            AcceptEmpty = 4,
        }

        public static string GetLine(GetLineOptions options, Func<string, bool> validator, string prompt)
        {
            string input = "";
            do
            {
                Console.Write($"{prompt} ");

                input = Console.ReadLine();

                if ((options & GetLineOptions.TrimSpaces) != 0) input = input.Trim();

                if ((options & GetLineOptions.AcceptEmpty) != 0 && (input == "" || input == null)) return "";

                if (input == "" || input == null)
                {
                    CliPrinter.Error("Empty value not allowed.");
                    continue;
                }

                if (!validator(input)) continue;
                else break;

            } while (true);

            return input;
        }

        [Flags]
        public enum GetIntOptions
        {
            AllowEmpty = 1,
        }
        public static int? GetInt(GetIntOptions options, Func<int, bool> rangeValidator, string prompt)
        {
            string input = "";
            Nullable<int> inputAsInt = null;

            do
            {
                Console.Write($"{prompt} ");

                input = Console.ReadLine();

                if ((options & GetIntOptions.AllowEmpty) != 0 && input.Trim() == "" || input == null) break;

                if (input == "" || input == null)
                {
                    CliPrinter.Error("Empty value not allowed.");
                    continue;
                }

                try
                {
                    var asInt = Int32.Parse(input);
                    if (!rangeValidator(asInt)) continue;
                    inputAsInt = asInt;
                    break;
                }
                catch
                {
                    CliPrinter.Error("Please enter a number.");
                    continue;
                }
            } while (true);

            return inputAsInt;
        }
    }
}