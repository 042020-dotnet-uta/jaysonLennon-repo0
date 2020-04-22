using System;
using System.Collections.Generic;

namespace RockPaperScissors
{


    class Game
    {
        // Total rounds played.
        public List<Round> rounds = new List<Round>();

        // The players in the game.
        public Player p1, p2;

        public void Run()
        {
            // Init random number generator and re-use so we don't keep
            // rolling the same numbers.
            Random rng = new Random();

            // Get player information.
            this.p1 = Player.FromConsole("Enter name for player 1: ");
            this.p2 = Player.FromConsole("Enter name for player 2: ");

            // Main game loop.
            do
            {
                // Add 1 since there will have been no rounds played on the first iteration.
                int roundNumber = rounds.Count + 1;

                // Play the round.
                Round round = new Round(this.p1, this.p2, roundNumber, rng);

                // Display the result for the round.
                Console.WriteLine(round.GetResultString());

                // Save the round data.
                rounds.Add(round);

            } while (this.p1.wins < 2 && this.p2.wins < 2);

            // # of ties is (number of rounds played - total wins);
            int ties = rounds.Count - (this.p1.wins + this.p2.wins);

            // Change word plurality based on number of ties.
            string tieDisplay = ties == 1 ? "tie" : "ties";
            if (this.p1.wins > this.p2.wins)
            {
                Console.WriteLine($"{this.p1.name} wins {this.p1.wins}-{this.p2.wins} with {ties} {tieDisplay}.");
            }
            else
            {
                Console.WriteLine($"{this.p2.name} wins {this.p2.wins}-{this.p1.wins} with {ties} {tieDisplay}.");
            }
        }
    }
}