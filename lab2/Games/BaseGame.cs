using lab2.GameAccounts;

namespace lab2.Games
{
    public abstract class BaseGame
    { 
        private static int gameIndex = 1234567890;
        public int GameID;
        public BaseAccount Winner;
        public BaseAccount Loser;
        public int Rating;
        public GameType Type;
        
        public BaseGame(BaseAccount winner, BaseAccount loser)
        {
            Winner = winner;
            Loser = loser;
            Rating = DefineRating();
            GameID = gameIndex++;
        }
        public abstract int DefineRating();
    }
}