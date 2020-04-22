
using System;

namespace RockPaperScissors
{
    // Possible outcomes of each round.
    enum RoundResult { Player1, Player2, Tie };

    // Choices available to players.
    enum Choice { Rock, Paper, Scissors };

    class Round
    {
        public RoundResult result = RoundResult.Tie;
        public int roundNumber;

        // The choices made by each player during this round.
        public Choice p1Choice, p2Choice;

        // The players that played in this round.
        public Player p1, p2;

        public static RoundResult DetermineWinner(Choice p1Choice, Choice p2Choice)
        {
            switch (p1Choice)
            {
                // P1 choice...
                case Choice.Rock:
                    switch (p2Choice)
                    {
                        // P2 choice...
                        case Choice.Scissors: return RoundResult.Player1;
                        case Choice.Paper: return RoundResult.Player2;
                        default: return RoundResult.Tie;
                    }
                // P1 choice...
                case Choice.Paper:
                    switch (p2Choice)
                    {
                        // P2 choice...
                        case Choice.Rock: return RoundResult.Player1;
                        case Choice.Scissors: return RoundResult.Player2;
                        default: return RoundResult.Tie;
                    }
                // P1 choice...
                case Choice.Scissors:
                    switch (p2Choice)
                    {
                        // P2 choice...
                        case Choice.Paper: return RoundResult.Player1;
                        case Choice.Rock: return RoundResult.Player2;
                        default: return RoundResult.Tie;
                    }
                default:
                    return RoundResult.Tie;
            }
        }

        public Round(Player p1, Player p2, int roundNumber, Random rng)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p1Choice = (Choice)rng.Next(3);
            this.p2Choice = (Choice)rng.Next(3);
            this.roundNumber = roundNumber;
            this.result = Round.DetermineWinner(p1Choice, p2Choice);

            // Save the win or loss count for each player.
            Round.AdjustPlayerWinLossCount(p1, p2, this.result);
        }

        public static void AdjustPlayerWinLossCount(Player p1, Player p2, RoundResult result)
        {
            switch (result)
            {
                case RoundResult.Tie: return;
                case RoundResult.Player1:
                    p1.wins++;
                    p2.losses++;
                    return;
                case RoundResult.Player2:
                    p2.wins++;
                    p1.losses++;
                    return;
                default: return;
            }
        }

        public string GetResultString()
        {
            // Temporary var to hold the name of the winner (if not a tie).
            string winner = "";
            switch (this.result)
            {
                case RoundResult.Tie:
                    return $"Round {this.roundNumber} - {this.p1.name} chose {this.p1Choice}, {this.p2.name} chose {this.p2Choice} - was a tie.";
                case RoundResult.Player1: winner = this.p1.name; break;
                case RoundResult.Player2: winner = this.p2.name; break;
                default:
                    break;
            }
            return $"Round {this.roundNumber} - {this.p1.name} chose {this.p1Choice}, {this.p2.name} chose {this.p2Choice} - {winner} won.";
        }
    }
}