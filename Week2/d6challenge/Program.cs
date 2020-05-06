using System;

namespace d6challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Track total number of occurrences.
            var numSweetNSalty = 0;
            var numSweet= 0;
            var numSalty = 0;

            // Loop 1 through 100
            for (var i = 1; i <= 100; i++)
            {
                // The module operator performs a division operation and then
                // returns the remainder. So (2 % 2) would return 0 since
                // (2 / 2) is 1 with no remainder. (3 % 2) would return 1 since
                // (3 / 2) is 1 with a remainder of 1 etc.

                // When the integer is divisible by both 3 and 5 (3 * 5 == 15)...
                if (i % 15 == 0) {
                    Console.WriteLine("sweet'nSalty");
                    numSweetNSalty++;
                }
                // When the integer is divisible by 3...
                else if (i % 3 == 0) {
                    Console.WriteLine("sweet");
                    numSweet++;
                // When the integer is divisible by 5...
                } else if (i % 5 == 0) {
                    Console.WriteLine("salty");
                    numSalty++;
                // Anything else
                } else {
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine($"There were {numSweetNSalty} sweet'nSalties, {numSweet} sweets, and {numSalty} salties.");
        }
    }
}
