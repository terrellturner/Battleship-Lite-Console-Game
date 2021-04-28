using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            Console.ReadLine();
            PlayerInfoModel activePlayer = CreatePlayer();
            PlayerInfoModel opponent = CreatePlayer();
            PlayerInfoModel winner = null;

            do
            {
                DisplayShotGrid(activePlayer);
                RecordPlayerShot(activePlayer, opponent);
                bool continueGame = GameLogic.PlayerStillActive(opponent);
                if (continueGame)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }

                else
                {
                    winner = activePlayer;
                }
            } while (winner == null);

            IdentifyWinner(winner);

            Console.ReadLine();
        }

        private static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations {winner.UserName}!");
            Console.WriteLine($"{winner.UserName}, it took you {(GameLogic.GetShotsTaken(winner))} shots to win.");
        }

        private static void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                try
                {
                    (row, column) = GameLogic.SplitShotString(shot);
                    isValidShot = GameLogic.ValidateShot(activePlayer, row, column);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValidShot = false;
                }

                if(isValidShot == false)
                {
                    Console.WriteLine("Warning! Invalid shot detected. Please try again.");
                }
            } while (isValidShot == false);

            bool isAHit = GameLogic.IdResult(opponent, row, column);
            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
        }

        private static string AskForShot(PlayerInfoModel player)
        {
            Console.WriteLine();
            Console.Write($"{player.UserName}, choose one of the spots above to shoot!: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.PlayerShots[0].SpotLetter;
            foreach (var gridSpot in activePlayer.PlayerShots)
            {
                if(gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                }
                if(gridSpot.SpotStatus == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
                }
                else if(gridSpot.SpotStatus == GridSpotStatus.Hit)
                {
                    Console.Write(" X  ");
                }
                else if(gridSpot.SpotStatus == GridSpotStatus.Miss)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.Write(" ?  ");
                }
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Battleship Lite!");
            Console.WriteLine("Programmed by Terrell Turner");
            Console.WriteLine("With extremely detailed guidance from Tim Corey!");
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue.");
        }
        private static PlayerInfoModel CreatePlayer()
        {
            PlayerInfoModel output = new PlayerInfoModel();
            output.UserName = AskForUsersName();
            GameLogic.InitializeGrid(output);
            PlaceShips(output);
            Console.Clear();
            return output;
        }
        private static string AskForUsersName()
        {
            Console.Write("Please enter your name: ");
            string output = Console.ReadLine();
            return output;
        }
        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where would you like to place ship number {model.PlayerSpots.Count() + 1}?: ");
                string location = Console.ReadLine();
                bool isValidLocation = false;
                try
                {
                     isValidLocation = GameLogic.PlaceShip(model, location);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (!isValidLocation)
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                }
            } while (model.PlayerSpots.Count() < 5);
        }
    }
}
