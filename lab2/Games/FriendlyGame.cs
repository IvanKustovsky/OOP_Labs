using lab2.GameAccounts;

namespace lab2.Games { 
    class FriendlyGame : BaseGame
    {
        public FriendlyGame(BaseAccount winner, BaseAccount loser) : base(winner, loser) {
            Type = GameType.Friendly;
        }

        //Для дружньої гри рейтинг не рахується
        public override int DefineRating()
        {
            return 0;
        }
    }
}
