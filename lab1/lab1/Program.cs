using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    public enum PossibleResult 
    {
        Lose,
        Win
    }
    public class Game {

        private static int gameIndex = 1234567890;
        public int GameID;
        public GameAccount winner;
        public GameAccount loser;
        public int Rating;
        public PossibleResult Result;

        public Game(GameAccount winner, GameAccount loser, int Rating, PossibleResult Result)
        {
            this.winner = winner;
            this.loser = loser;
            this.Rating = Rating;
            this.Result = Result;
            GameID = gameIndex++;
        }
    }
    
    public class GameAccount
    {
        public GameAccount(string Name)
        {
            UserName = Name;
        }
        private List<Game> allGames = new List<Game>();
        public string UserName { get; }
        public int CurrentRating { get
            {
                int countRating = 1;
                foreach (var item in allGames)
                {
                    if (this == item.winner)
                    {
                        countRating += item.Rating;
                    }
                    else {
                        countRating = Math.Max(1, countRating - item.Rating);
                    }
                }
                return countRating;
            } 
        }
        
        public int GamesCount { 
            get {
                int count = 0;
                foreach (var item in allGames)
                {
                    count++;
                }
                return count;
            } }

        public void WinGame(GameAccount opponent, int rating) 
        {
            if(rating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be positive.");
            }
            var game = new Game(this, opponent, rating, PossibleResult.Win);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }

        public void LoseGame(GameAccount opponent, int rating)
        {
            if (rating < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be positive.");
            }
            var game = new Game (opponent, this, rating, PossibleResult.Lose);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }

        public string GetStats() {
            var report = new StringBuilder();
            report.AppendLine("Statistics of the player " + this.UserName);
            report.AppendLine("Opponent Name\tResult\tRating\tIndex of game");
                foreach (var item in allGames)
                {
                var opponent = this == item.winner ? item.loser : item.winner;
                PossibleResult result = this == item.winner ? PossibleResult.Win : PossibleResult.Lose ;
                    report.AppendLine($"     {opponent.UserName}\t {(PossibleResult)result}\t  {item.Rating}\t{item.GameID}");
                }
            return report.ToString();
        }

        
    }
    class Program
    {

        static void Main(string[] args)
        {
            GameAccount player1 = new GameAccount("Ivan");
            GameAccount player2 = new GameAccount("Zhen");
            Console.WriteLine("Current rating of " + player1.UserName + ": " + player1.CurrentRating);
            Console.WriteLine("Current rating of " + player2.UserName +": " + player2.CurrentRating);
            Console.WriteLine(player1.UserName +" played " + player1.GamesCount +" games.");
            Console.WriteLine(player2.UserName + " played " + player2.GamesCount + " games.");
            player1.WinGame(player2, 5);
            player1.LoseGame(player2, 3);
            player2.WinGame(player1, 4);
            player1.WinGame(player2, 7);
            player1.WinGame(player2, 8);
            player2.WinGame(player1, 7);
            Console.WriteLine(player1.GetStats());
            Console.WriteLine(player2.GetStats());
            Console.WriteLine(player1.UserName + " played " + player1.GamesCount + " games.");
            Console.WriteLine(player2.UserName + " played " + player2.GamesCount + " games.");
            Console.WriteLine("Current rating of " + player1.UserName + ": " + player1.CurrentRating);
            Console.WriteLine("Current rating of " + player2.UserName + ": " + player2.CurrentRating);

            GameAccount invalid = new GameAccount("invalid");
            try {
                invalid.WinGame(player2, -10);
                  }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }

        }
    }
}
