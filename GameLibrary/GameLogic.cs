using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        //InitializeGrid(PlayerInfoModel model)
        //List<string>, letters{}
        //List<int>, numbers{}

        //foreach(string letter in letters)
        //{foreach(int num in numbers){AddGridSpot(PIM model, string letter, string num}

        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int num in numbers)
                {
                    AddGridSpot(model, letter, num);
                }
            }
        }

        public static void AddGridSpot(PlayerInfoModel model, string letter, int num)
        {
            GridSpotModel gridSpot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = num,
                SpotStatus = GridSpotStatus.Empty
            };
            model.PlayerShots.Add(gridSpot);
        }

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;
            foreach (var ship in player.PlayerSpots)
            {
                if (ship.SpotStatus != GridSpotStatus.Sunk)
                {
                    isActive = true;
                    return isActive;
                }
            }

            return isActive;
            //bool isActive = false;
            //foreach{
            //if(ship status != sunk){
            //isActive = true}
            //}
            //return isActive

            //PRE SOLUTION
            ////int playerLossCounter = 0;

            ////foreach (GridSpotModel gridSpot in opponent.PlayerSpots)
            ////{
            ////    if(gridSpot.SpotStatus == GridSpotStatus.Hit)
            ////    {
            ////        playerLossCounter++;
            ////    }
            ////}
            ////if(playerLossCounter >= 5)
            ////{
            ////    return false;
            ////}
            ////else
            ////{
            ////    return true;
            ////}
        }

        public static int GetShotsTaken(PlayerInfoModel player)
        {
            int hitCounter = 0;
            foreach (GridSpotModel gridSpot in player.PlayerShots)
            {
                if (gridSpot.SpotStatus == GridSpotStatus.Hit)
                {
                    hitCounter++;
                }
            }
            return hitCounter;
        }

        public static (string row, int column) SplitShotString(string shot)
        {
            //string row = ""
            //int column = 0
            //if(shot.Length !=2){
            //throw new exception}
            //char[] shotArray = shot.ToCharArray();
            //row = shotArray[0].ToString 
            //

            string row = "";
            int column = 0;

            if (shot.Length != 2)
            {
                throw new ArgumentException("ERROR: Shot input should only be 2 characters.");
            }

            char[] shotArray = shot.ToCharArray();

            row = shotArray[0].ToString().ToUpper();
            column = int.Parse(shotArray[1].ToString());

            return (row, column);

            ////string[] storedShot = new string[2];
            ////storedShot = shot.Split();
            ////string row = storedShot[0];
            ////int column;
            ////int.TryParse(storedShot[1], out column);
            ////return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool isValidLocation = false;
            foreach (var gridSpot in player.PlayerShots)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if(gridSpot.SpotStatus == GridSpotStatus.Empty)
                    {
                        isValidLocation = true;
                        return isValidLocation;
                    }
                }
            }
            return isValidLocation;

            //PRE SOLUTION
            //foreach (var gridSpot in activePlayer.PlayerShots)
            //{
            //    if (gridSpot.SpotStatus != GridSpotStatus.Hit ||
            //        gridSpot.SpotStatus != GridSpotStatus.Sunk ||
            //        gridSpot.SpotStatus != GridSpotStatus.Miss)
            //    {
            //        if (gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;
        }

        //Set boolSwitch to TRUE if trying to validate. E.g. Player enters ship "A1", will return true unless A1 is occupied.
        //Set boolSwitch to FALSE if trying to find and compare. E.g. Player shoots at a specific spot and will return false unless it's valid.
        public static bool FindPlayerSpot(PlayerInfoModel player, string row, int column, bool boolSwitch)
        {
            bool output = false;

            if (boolSwitch == true)
            {
                output = true;
            }

            foreach (var ship in player.PlayerSpots)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    if(boolSwitch == true)
                    {
                        output = false;
                        return output;
                    }
                    output = true;
                    return output;
                }
            }
            return output;
        }

        public static GridSpotModel FindShip(PlayerInfoModel player, string row, int column)
        {
            GridSpotModel gridSpot = new GridSpotModel();
            foreach (var ship in player.PlayerSpots)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    gridSpot = ship;
                }
            }

            return gridSpot;
        }

        public static bool IdResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit;
            GridSpotModel ship;

            isAHit = FindPlayerSpot(opponent, row, column, false);
            ship = FindShip(opponent, row, column);

            if (isAHit)
            {
                ship.SpotStatus = GridSpotStatus.Sunk;
            }

            return isAHit;

            //PRE SOLUTION
            //bool isAHit = false;
            //foreach (GridSpotModel gridSpot in opponent.PlayerSpots)
            //{
            //    if(gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
            //    {
            //        if (gridSpot.SpotStatus == GridSpotStatus.Ship)
            //        {
            //            isAHit = true;
            //            gridSpot.SpotStatus = GridSpotStatus.Hit;
            //        }
            //        else if(gridSpot.SpotStatus == GridSpotStatus.Empty)
            //        {
            //            isAHit = true;
            //            gridSpot.SpotStatus = GridSpotStatus.Miss;
            //        }
            //    }
            //}
            //return isAHit;
        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in player.PlayerShots)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Hit;
                        Console.WriteLine($"{player.UserName}, you hit a ship at {row}{column}!");
                        Console.WriteLine();
                    }
                    else
                    {
                        gridSpot.SpotStatus = GridSpotStatus.Miss;
                        Console.WriteLine($"{player.UserName}, {row}{column} was a miss!");
                        Console.WriteLine();
                    }
                }
            }
        }

        public static bool PlaceShip(PlayerInfoModel model, string location)
        {
            bool output = false;
            (string row, int column) = SplitShotString(location);
            //bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isValidLocation = FindPlayerSpot(model, row, column, true);
            bool isSpotOpen = FindPlayerSpot(model, row, column, true);

            if (isValidLocation && isSpotOpen)
            {
                model.PlayerSpots.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    SpotStatus = GridSpotStatus.Ship
                });
                output = true;
            }

            return output;

            //if(isValidLocation && isSpotOpen){
            //model.PlayerSpots.Add(new GridSpotModel
            //SpotLetter = row.ToUpper
            //SpotNumber = column
            //SpotStatus = ship
            //output = true}
            //return output

            //PRE SOLUTION
            //string row = "";
            //int column;

            //string[] shipPlacement = location.Split();

            //row = shipPlacement[0];
            //bool validColumn = int.TryParse(shipPlacement[1], out column);
            //if (validColumn == false)
            //{
            //    return false;
            //}

            //foreach (GridSpotModel gridSpot in model.PlayerSpots)
            //{
            //    if(gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
            //    {
            //        gridSpot.SpotStatus = GridSpotStatus.Ship;
            //        return true;
            //    }
            //}
            //return false;
        }

        //private static bool ValidateGridLocation(PlayerInfoModel model, string row, int column)
        //{
        //    bool isValidLocation = false;
        //    foreach (var ship in model.PlayerShots)
        //    {
        //        if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
        //        {
        //            isValidLocation = true;
        //            return isValidLocation;
        //        }
        //    }
        //    return isValidLocation;
        //}
        //    private static bool ValidateShipLocation(PlayerInfoModel model, string row, int column)
        //    {
        //        //bool isValidLocation = true
        //        //foreach(var ship in model.PlayerSpots
        //        //if(ship.SpotLetter == row.ToUpper && ship.SpotNumber == column){
        //        //isValidLocation == false}
        //        //return isValidLocation

        //        bool isValidLocation;

        //        isValidLocation = FindPlayerShip(model, row, column, true);
        //        return isValidLocation;
        //    }
    }


}
