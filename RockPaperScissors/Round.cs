
using System;

namespace RockPaperScissors {
    class Round {
        public RoundResult result = RoundResult.Tie;
        public Choice p1Choice, p2Choice;
        public Player p1, p2;
        public int roundNumber;

		public static RoundResult DetermineWinner(Choice p1Choice, Choice p2Choice) {
			switch (p1Choice)
			{
				case Choice.Rock:
					if(p2Choice == Choice.Scissors)
					{
						return RoundResult.Player1;
					}
					else if (p2Choice == Choice.Paper)
					{
						return RoundResult.Player2;
					}
					else
					{
						return RoundResult.Tie;
					}
				case Choice.Paper:
					if(p2Choice == Choice.Rock)
					{
						return RoundResult.Player1;
					}
					else if (p2Choice == Choice.Scissors)
					{
						return RoundResult.Player2;
					}
					else
					{
						return RoundResult.Tie;
					}
				case Choice.Scissors:
					if(p2Choice == Choice.Paper)
					{
						return RoundResult.Player1;
					}
					else if (p2Choice == Choice.Rock)
					{
						return RoundResult.Player2;
					}
					else
					{
						return RoundResult.Tie;
					}

				default:
					return RoundResult.Tie;
			}
        }

        public Round(Player p1, Player p2, int roundNumber, Random rng) {
            this.p1 = p1;
            this.p2 = p2;
            this.p1Choice = (Choice)rng.Next(3);
            this.p2Choice = (Choice)rng.Next(3);
            this.roundNumber = roundNumber;
            this.result = Round.DetermineWinner(p1Choice, p2Choice);

            // Save the win or loss count for each player.
            Round.AdjustPlayerWinLossCount(p1, p2, this.result);
        }

        public static void AdjustPlayerWinLossCount(Player p1, Player p2, RoundResult result) {
            switch (result) {
                case RoundResult.Tie:
                    return;
                case RoundResult.Player1:
                    p1.wins++;
                    p2.losses++;
                    return;
                case RoundResult.Player2:
                    p2.wins++;
                    p1.losses++;
                    return;
                default:
                    return;
            }
        }

        public string GetResultString() {
            switch (this.result) {
                case RoundResult.Tie:
                    return $"Round {this.roundNumber} - {this.p1.name} chose {this.p1Choice}, {this.p2.name} chose {this.p2Choice} - was a tie.";
                case RoundResult.Player1:
                    return $"Round {this.roundNumber} - {this.p1.name} chose {this.p1Choice}, {this.p2.name} chose {this.p2Choice} - {this.p1.name} won.";
                case RoundResult.Player2:
                    return $"Round {this.roundNumber} - {this.p1.name} chose {this.p1Choice}, {this.p2.name} chose {this.p2Choice} - {this.p2.name} won.";
                default:
                    return "";
            }
        }
    }
}