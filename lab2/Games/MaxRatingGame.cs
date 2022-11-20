using lab2.GameAccounts;
using System;

namespace lab2.Games
{
    class MaxRatingGame : BaseGame
    {
        public MaxRatingGame(BaseAccount winner, BaseAccount loser) : base(winner, loser) {
            Type = GameType.MaxRating;
        }

        //Рейтинг визначається як максимальний серед двох гравців
        public override int DefineRating()
        {
            return Math.Max(Winner.CurrentRating, Loser.CurrentRating);
        }
    }
}
