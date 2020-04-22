using System;
using System.Collections.Generic;

namespace RockPaperScissors
{
	//Enum for choices
	enum Choice { Rock, Paper, Scissors };
	class Program
	{
		static void Main(string[] args)
		{
			/*
			Requirements:
			1. Write pseudocode for a Rock/Paper/Scissors game.
			2. Get names from the users.
			3. Play a game of best 2 of 3 rounds. Each "choice" is a random assignment
			4. Report the winner and the results of each round.
			5. Report the ties as part of the output. "Round 1 - John chose paper, Jim chose rock. - John won"
			6. continue till one player has 2 points.
			7. "John wins 2-1 with 3 ties."
			*/

			//Use Random class number generator
			Random randNum = new Random();

			//variables for 2 user names
			string player1Name = "";
			string player2Name = "";

			//variables for number of wins for each player
			int player1Wins = 0;
			int player2Wins = 0;

			//variables for each player's choice (array or list for each player)
			List<Choice> player1Choices = new List<Choice>();
			List<Choice> player2Choices = new List<Choice>();

			//list for result of each round
			List<string> result = new List<string>();

			//get player 1 name
			Console.Write("Enter name for player 1: ");
			player1Name = Console.ReadLine();

			//get player 2 name
			Console.Write("Enter name for player 2: ");
			player2Name = Console.ReadLine();

			//prompt players to start game (possible additional requirement not listed in the written version)
			//Console.WriteLine("Press any key to start");
			//Console.ReadKey();

			//start game loop do
			do
			{
				////generate and store random choice for player 1 
				int roll1 = randNum.Next(3);
				player1Choices.Add((Choice)roll1);
				////generate and store random choice for player 2 
				int roll2 = randNum.Next(3);
				player2Choices.Add((Choice)roll2);
				////determine outcome and store result
				/*
				use switch statement that checks user1 choice
				case rock: if user2 choice is scissor user1 win. if user2 choice is paper user2 win.
				case paper: if user2 choice is rock user1 win. if user2 choice is scissor user2 win.
				case scissor: if user2 choice is paper user1 win. if user2 choice is rock user2 win.
				default : nothing
				*/
				
				switch ((Choice)roll1)
				{
					case Choice.Rock:
						if((Choice)roll2 == Choice.Scissors)
						{
							result.Add(player1Name);
							////if not tie increment winner counter
							player1Wins++;
						}else if ((Choice)roll2 == Choice.Paper)
						{
							result.Add(player2Name);
							////if not tie increment winner counter
							player2Wins++;
						}
						else
						{
							// using null for a tie
							result.Add(null);
						}
						break;
					case Choice.Paper:
						if ((Choice)roll2 == Choice.Rock)
						{
							result.Add(player1Name);
							player1Wins++;
						}
						else if ((Choice)roll2 == Choice.Scissors)
						{
							result.Add(player2Name);
							player2Wins++;
						}
						else
						{
							result.Add(null);
						}
						break;
					case Choice.Scissors:
						if ((Choice)roll2 == Choice.Paper)
						{
							result.Add(player1Name);
							player1Wins++;
						}
						else if ((Choice)roll2 == Choice.Rock)
						{
							result.Add(player2Name);
							player2Wins++;
						}
						else
						{
							result.Add(null);
						}
						break;

					default:
						break;
				}

			} while (player1Wins < 2 && player2Wins < 2);
			
			//while player1wins less than 2 and player2wins less than 2

			/*iterate through results and print message in following format : 
			"Round [#] '-' user1 chose [choice], user2 chose [choice]. '-' [winner] won" 
			Example: "Round 1 - John chose paper, Jim chose rock. - John won"
			*/
			//print results
			for(int i = 0; i < player1Choices.Count; i++)
			{
				// A null player in results indicates a tie.
				if (result[i] == null)
				{
					Console.WriteLine($"Round {i + 1} - {player1Name} chose {player1Choices[i]}, {player2Name} chose {player2Choices[i]} - was a tie.");
				}
				else
				{
					Console.WriteLine($"Round {i + 1} - {player1Name} chose {player1Choices[i]}, {player2Name} chose {player2Choices[i]} - {result[i]} won.");
				}				
			}

			// Calculate the number of ties by subtracting the sum of player1 & player2
			// wins from the total number of rounds played.
			int ties = player1Choices.Count - (player1Wins + player2Wins);

			/*print final statement in following format : 
			"[winner] wins [user1wins] '-' [user2wins] with [length of results - ([user1wins] + [user2wins])] ties."*/
			if (player1Wins > player2Wins)
			{
				Console.WriteLine($"{player1Name} wins {player1Wins}-{player2Wins} with {ties} ties.");
			}
			else
			{
				Console.WriteLine($"{player2Name} wins {player2Wins}-{player1Wins} with {ties} ties.");
			}
		}
	}
}
