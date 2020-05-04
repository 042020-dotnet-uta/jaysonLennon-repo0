using System;
using System.Collections.Generic;

namespace CodingChallengeWeek3
{
    class Program
    {

        /// <summary>
        /// Enumerator containing all possible menu options.
        /// </summary>
        public enum MenuChoice
        {
            IsEven,
            MultTable,
            Shuffle,
            Exit
        }

        /// <summary>
        /// Displays the menu options to the user.
        /// </summary>
        public static void PrintMenu()
        {
            Console.Write("\n");
            Console.WriteLine("1. Is the number even?");
            Console.WriteLine("2. Multiplication Table");
            Console.WriteLine("3. Alternating Elements");
            Console.WriteLine("4. Exit");
        }

        /// <summary>
        /// Input loop prompting the user to select a menu option as indicated in PrintMenu().
        /// </summary>
        /// <returns>Menu that the user selected.</returns>
        public static MenuChoice GetMenuChoice()
        {
            do
            {
                Console.Write("\nOption: ");

                var choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        return MenuChoice.IsEven;
                    case "2":
                        return MenuChoice.MultTable;
                    case "3":
                        return MenuChoice.Shuffle;
                    case "4":
                        return MenuChoice.Exit;
                    case "EXIT":
                        return MenuChoice.Exit;
                    default:
                        Console.WriteLine("Please choose a menu option or type EXIT to quit.");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to enter a number and, if possible, determines if it is even or odd.
        ///
        /// If they enter a number, it will display whether the number is even or odd.
        /// If they do not enter a number, it will display a message indicating as such.
        /// If the user enters 'EXIT' or a blank entry, control will be returned to the main menu.
        /// </summary>
        public static void IsEven()
        {
            Console.Write("\nIs the number even?\n");
            do
            {
                Console.Write("\nEnter a number: ");

                var input = Console.ReadLine();

                // TryParse will populate 'number' if it returns true.
                int number;
                var isNumber = Int32.TryParse(input, out number);

                if (isNumber)
                {
                    // Remainder division determines if the number is even or odd.
                    if (number % 2 == 0)
                    {
                        Console.WriteLine("That number is even");
                    }
                    else
                    {
                        Console.WriteLine($"{number} is not an even number.");
                    }
                }
                else
                {
                    // If the input is 'EXIT', or no input is entered, then exit to the main menu.
                    if (input.ToUpper() == "EXIT" || input == "" || input == null) return;

                    // If the input is anything else, notify the user and try again.
                    Console.WriteLine($"'{input}' is a string, not a number.");
                    continue;

                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to enter a number and, if possible, prints a multiplication table.
        ///
        /// If they enter a number, it will display the table up to the number provided.
        /// If they do not enter a number, it will display a message indicating as such.
        /// If the user enters 'EXIT' or a blank entry, control will be returned to the main menu.
        /// </summary>
        public static void MultTable()
        {
            Console.Write("\nMultiplication Table\n");
            do
            {
                Console.Write("\nEnter a number: ");

                var input = Console.ReadLine();

                // TryParse will populate 'number' if it returns true.
                int number;
                var isNumber = Int32.TryParse(input, out number);

                if (isNumber)
                {
                    // Hardcode 0 so we have a simpler loop later.
                    if (number == 0)
                    {
                        Console.WriteLine("0x0 = 0");
                    }
                    // We only want positive numbers.
                    else if (number < 0)
                    {
                        Console.WriteLine("Please enter a positive number.");
                    }

                    // Two loops needed to print every possible permutation.
                    // The outer loop is the LHS of the expression and the
                    // inner loop is the RHS of the expression.
                    for (var o = 1; o <= number; o++)
                    {
                        for (var i = 1; i <= number; i++)
                        {
                            Console.WriteLine($"{o}x{i} = {o * i}");
                        }
                    }
                }
                else
                {
                    // If the input is 'EXIT', or no input is entered, then exit to the main menu.
                    if (input.ToUpper() == "EXIT" || input == "" || input == null) return;

                    // If the input is anything else, notify the user and try again.
                    Console.WriteLine($"'{input}' is a string, not a number.");
                    continue;
                }
            } while (true);
        }

        /// <summary>
        /// Gets a sequence of 5 elements from the user.
        /// If the user does not enter any data, control is returned
        /// to the calling function.
        /// </summary>
        /// <returns>A List containing the data entered; null if the user aborted the process with a blank entry.</returns>
        public static List<string> GetList()
        {
            var list = new List<string>();

            // 'entryNumber' tracks which entry the user is on. This is so we can provide
            // feedback to the user.
            var entryNumber = 1;
            do
            {
                Console.Write($"Entry {entryNumber}: ");
                var element = Console.ReadLine();

                // If no input is entered, then exit to the main menu.
                if (element == "" || element == null)
                {
                    Console.WriteLine("\nEmpty data. Aborting.");
                    return null;
                }

                list.Add(element);

                entryNumber += 1;

            // Continue until we get 5.
            } while (list.Count < 5);

            return list;
        }

        /// <summary>
        /// Prompts the user to enter two sequences of data, and then outputs the data
        /// from each sequence, alternating between the entries of each sequence.
        ///
        /// If no data is entered, control is returned to the main menu.
        /// </summary>
        public static void Shuffle()
        {
            Console.Write("\nAlternating Elements\n\n");

            Console.WriteLine("Input data for list 1:");
            var list1 = GetList();

            // list1 will be null if the user decided to abort via an empty entry.
            if (list1 == null) return;


            Console.WriteLine("\nInput data for list 2:");
            var list2 = GetList();

            // list2 will be null if the user decided to abort via an empty entry.
            if (list2 == null) return;

            var shuffled = new List<string>();
            for (var i = 0; i < list1.Count; i++)
            {
                // Add an element from each list into our 'shuffled' list
                // to result in alternating list elements.
                shuffled.Add(list1[i]);
                shuffled.Add(list2[i]);
            }

            // We use String.Join to concat all the list elements with
            // a comma between each one. Then just drop it between
            // braces to satisfy the output requirement.
            Console.WriteLine($"\n[{String.Join(',', shuffled)}]");
        }

        static void Main(string[] args)
        {
            do
            {
                // Print out the menu so the user knows which options are available.
                PrintMenu();
                // Determine which choice the user picked.
                var choice = GetMenuChoice();
                switch (choice)
                {
                    case MenuChoice.IsEven:
                        IsEven();
                        break;
                    case MenuChoice.MultTable:
                        MultTable();
                        break;
                    case MenuChoice.Shuffle:
                        Shuffle();
                        break;
                    case MenuChoice.Exit:
                        return;
                }
            } while (true);
        }
    }
}
