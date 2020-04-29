using System;

namespace delegates
{
    class Program
    {
        public delegate int Adder(int v);

        static int AddOne(int v) => v+1;
        static int AddTwo(int v) => v + 2;

        static void Main(string[] args)
        {
            Adder add1 = AddOne;
            Adder add2 = AddTwo;
            Adder should_be_three = add1 + add2;
            var three = should_be_three(1);

            Console.WriteLine($"{three}");
        }
    }
}
