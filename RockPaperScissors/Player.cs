using System;

namespace RockPaperScissors {
    class Player {
        public string name;
        public int wins, losses;

        public Player(string name) {
            this.name = name;
        }

        public static Player FromConsole(int playerNumber) {
			Console.Write($"Enter name for player {playerNumber}: ");
            return new Player(Console.ReadLine());
        }
    }
}