
namespace project.Games
{
    class GameVsBot : Game
    {
        public GameVsBot(GameType type) //Перевизначення гри (гра проти бота)
        {
            GameType = type.ToString();
            FirstToMovePlayer = "X";
            SecondToMovePlayer = "O";
            Rating = 0;
            Result = PlayGameVsBot();
            GameResult = Result.ToString();
        }
    }
}
