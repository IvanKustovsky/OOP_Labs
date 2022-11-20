using System;
using System.Collections.Generic;
using System.Text;
using lab2.Games;

namespace lab2.GameAccounts
{
    public enum PossibleResult
    {
        Lose,
        Win
    }
    public class BaseAccount
    {
        public BaseAccount(string Name)
        {
            UserName = Name;
        }
        private readonly GamesFactory factory = new GamesFactory();
        protected List<BaseGame> allGames = new List<BaseGame>();
        public string UserName { get; }
        public virtual int CurrentRating
        {
            get
            {
                int countRating = 1;
                foreach (var item in allGames)
                {
                    if (this == item.Winner)
                    {
                        countRating += item.Rating;
                    }
                    else
                    {
                        countRating = Math.Max(1, countRating - item.Rating);
                    }
                }
                return countRating;
            }
        }
        public void WinGame(BaseAccount opponent, GameType type)
        {
            if (opponent == this) {
                throw new ArgumentException(nameof(opponent), "You can't play against yourself");
            }
            BaseGame game = factory.CreateNewGame(type, winner:this, loser:opponent);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }

        public void LoseGame(BaseAccount opponent, GameType type)
        {
            if (opponent == this)
            {
                throw new ArgumentException(nameof(opponent), "You can't play against yourself");
            }
            BaseGame game = factory.CreateNewGame(type, winner:opponent, loser:this);
            allGames.Add(game);
            opponent.allGames.Add(game);
        }

        public string GetStats()
        {
            var report = new StringBuilder();
            report.AppendLine("Statistics of the player " + UserName);
            report.AppendLine("Opponent Name\tResult\tType of Game\tRating\tIndex of game");
            foreach (var item in allGames)
            {
                var opponent = this == item.Winner ? item.Loser : item.Winner;
                PossibleResult result = this == item.Winner ? PossibleResult.Win : PossibleResult.Lose;
                report.AppendLine($"     {opponent.UserName}\t {(PossibleResult)result}\t  {item.Type}\t  {item.Rating}\t{item.GameID}");
            }
            return report.ToString();
        }

    }
}
