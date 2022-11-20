using lab2.GameAccounts;
using System;

namespace lab2.Games
{
    class StandardGame : BaseGame
    {
        public StandardGame(BaseAccount winner, BaseAccount loser) : base(winner, loser) {
            Type = GameType.Standard;
        }
        public override int DefineRating()
        {
            return 5 + Math.Abs((Winner.CurrentRating - Loser.CurrentRating) / 2);
        }
    }
}
