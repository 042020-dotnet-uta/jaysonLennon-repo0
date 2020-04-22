using System;

namespace RockPaperScissors
{
    class Player
    {
        public string name;
        public int wins, losses;

        public Player(string name)
        {
            this.name = name;
        }

        public static Player FromConsole(string prompt)
        {
            Console.Write(prompt);
            return new Player(Console.ReadLine());
        }
    }
}