using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace RockPaperScissors
{

    class Game
    {
        // Total rounds played.
        public List<Round> rounds = new List<Round>();

        // The players in the game.
        public Player p1, p2;

        private readonly ILogger _logger;
        public Game(ILogger<Game> logger)
        {
            this._logger = logger;
        }

        public void Run()
        {
            _logger.LogTrace("Running game");
            // Init random number generator and re-use so we don't keep
            // rolling the same numbers.
            Random rng = new Random();

            // Get player information.
            this.p1 = NewPlayer(1);
            this.p2 = NewPlayer(2);

            // Main game loop.
            _logger.LogTrace("Begin game loop");
            do
            {
                // Add 1 since there will have been no rounds played on the first iteration.
                int roundNumber = rounds.Count + 1;

                // Play the round.
                _logger.LogTrace($"Playing round number {roundNumber}");
                Round round = new Round(this.p1, this.p2, roundNumber, rng, _logger);

                // Display the result for the round.
                Console.WriteLine(round.GetResultString());

                // Save the round data.
                rounds.Add(round);

            } while (this.p1.wins < 2 && this.p2.wins < 2);
            _logger.LogTrace("Game over");

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

        private Player NewPlayer(int playerNumber) {
            _logger.LogTrace($"Get player name for player number {playerNumber}");
            return Player.FromConsole($"Enter name for player {playerNumber}: ");
        }
    }
}