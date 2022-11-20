using lab2.GameAccounts;

namespace lab2.Games
{
    public enum GameType { 
    Friendly,   //Дружня(тренувальна) гра
    Standard,   //Звичайна гра
    MaxRating   //Гра на максимальний рейтинг серед гравців
    }
    class GamesFactory
    {
        public BaseGame CreateNewGame(GameType type, BaseAccount winner, BaseAccount loser) {
            if (type == GameType.Standard) {
                return new StandardGame(winner, loser);
            }
            if (type == GameType.Friendly)
            {
                return new FriendlyGame(winner, loser);
            }
                return new MaxRatingGame(winner, loser);
            
        }
    }
}
