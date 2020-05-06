using System;
using System.Text.RegularExpressions;

/// <summary>
/// Utility classes for working with the CLI.
/// </summary>
namespace Util
{
    /// <summary>
    /// Extension methods for <c>CliMenu</c>.
    /// </summary>
    public static class MenuExtensions
    {
        /// <summary>
        /// Prints an error message then waits for the user to confirm, after which
        /// the menu will be taken off the menu stack causing navigation to back one menu.
        /// </summary>
        /// <param name="menu">The current menu.</param>
        /// <param name="message">Message to print to the user.</param>
        public static void AbortThenExit(this CliMenu menu, string message)
        {
            CliPrinter.Error(message);
            CliInput.PressAnyKey();
            menu.MenuExit();
        }
    }

    /// <summary>
    /// Performs various string formatting to provide a uniform look across application.
    /// </summary>
    public class CliFormatter
    {
        /// <summary>
        /// Formats a title.
        /// </summary>
        /// <param name="title">The text to be formatted.</param>
        /// <returns>The formatted text.</returns>
        public static string Title(string title)
        {
            return $"=== {title} ===\n";
        }

        /// <summary>
        /// Formats an error message.
        /// </summary>
        /// <param name="message">The text to be formatted.</param>
        /// <returns>The formatted text.</returns>
        public static string Error(string message)
        {
            return $"\n{message}\n";
        }
    }

    /// <summary>
    /// Prints out information to the console.
    /// </summary>
    public class CliPrinter
    {
        /// <summary>
        /// Print out a title.
        /// </summary>
        /// <param name="title">The text to print.</param>
        public static void Title(string title)
        {
            Console.WriteLine(CliFormatter.Title(title));
        }

        /// <summary>
        /// Print out an error.
        /// </summary>
        /// <param name="message">The text to print.</param>
        public static void Error(string message)
        {
            Console.WriteLine(CliFormatter.Error(message));
        }
    }

    /// <summary>
    /// Handles CLI input in various ways and wraps common functionality.
    /// </summary>
    public class CliInput
    {
        /// <summary>
        /// Displays a "Press any key" prompt, after displaying a custom message
        /// and waits for the user to press a key.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void PressAnyKey(string message)
        {
            Console.WriteLine(message);
            PressAnyKey();
        }
        /// <summary>
        /// Display a "Press any key" prompt and waits for the user to press a key.
        /// </summary>
        public static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
        /// <summary>
        /// Validates an email address.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>Whether the provided text is in an email address format.</returns>
        public static bool EmailValidator(string email)
        {
            if (!StoreDb.Customer.ValidateEmail(email))
            {
                CliPrinter.Error("Please enter a valid email address.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Prompts the user for a password while printing a custom prompt.
        /// The password will not be echoed to the terminal.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <returns>The password entered by the user.</returns>
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

        /// <summary>
        /// Options available for the <c>GetLine()</c> method.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=netcore-3.1
        /// </summary>
        [Flags]
        public enum GetLineOptions
        {
            /// <summary>Whether spaces should be trimmed from the input.</summary>
            TrimSpaces = 1,
            /// <summary>Whether an empty input is allowed.</summary>
            AcceptEmpty = 4,
        }

        /// <summary>
        /// Gets input from the user and optionally validates it given specific rules.
        /// The method will continue to prompt the user to enter a new value if it does
        /// not pass validation rules and configuration options.
        ///
        /// By default, this method does not accept an empty input.
        /// Use <c>GetLineOptions</c>.AcceptEmpty to change this behavior.
        /// </summary>
        /// <param name="options">Options to change behavior of the method.</param>
        /// <param name="validator">Validation method to run on the final input.</param>
        /// <param name="prompt">Prompt to display to the user.</param>
        /// <returns>A pre-validated string.</returns>
        public static string GetLine(GetLineOptions options, Func<string, bool> validator, string prompt)
        {
            string input = "";
            do
            {
                Console.Write($"{prompt} ");

                input = Console.ReadLine();

                if ((options & GetLineOptions.TrimSpaces) != 0) input = input.Trim();

                if ((options & GetLineOptions.AcceptEmpty) != 0 && (input == "" || input == null)) return "";

                if (input == "" || input == null) continue;

                if (!validator(input)) continue;
                else break;

            } while (true);

            return input;
        }

        /// <summary>
        /// Options for the <c>GetInt()</c> function.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=netcore-3.1
        /// </summary>
        [Flags]
        public enum GetIntOptions
        {
            /// <summary>Abort then an empty string is provided</summary>
            AllowEmpty = 1,
        }
        /// <summary>
        /// Prompts the user to provide an int, with optional validation rules.
        /// Be default this method requires the user to enter a value, use
        /// GetIntOptions.AllowEmpty to change this behavior.
        /// </summary>
        /// <param name="options">Options to change the behavior of this method.</param>
        /// <param name="rangeValidator">Validation function to determine if the value is within a specific range.</param>
        /// <param name="prompt">Prompt to display to the user.</param>
        /// <returns>A pre-validated <c>int</c>, or <c>null</c> if the user aborted the entry (and this is enabled in the options).</returns>
        public static int? GetInt(GetIntOptions options, Func<int, bool> rangeValidator, string prompt)
        {
            string input = "";
            Nullable<int> inputAsInt = null;

            do
            {
                Console.Write($"{prompt} ");

                input = Console.ReadLine();

                if ((options & GetIntOptions.AllowEmpty) != 0 && input.Trim() == "" || input == null) break;

                if (input == "" || input == null) continue;

                try
                {
                    var asInt = Int32.Parse(input);
                    if (!rangeValidator(asInt)) continue;
                    inputAsInt = asInt;
                    break;
                }
                catch
                {
                    continue;
                }
            } while (true);

            return inputAsInt;
        }
    }
}