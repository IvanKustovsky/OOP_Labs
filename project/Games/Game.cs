using Newtonsoft.Json;
using project.AccountFolder;

namespace project.Games
{
    enum GameType
    {
        online,
        training
    }
     class Game : GameLogic
    {
        [JsonProperty] public string FirstToMovePlayer { get; private set; }
        [JsonProperty] public string SecondToMovePlayer { get; private set; }
        [JsonProperty] public PossibleResult Result { get; private set; }
        [JsonProperty] public int Rating { get; private set; }
        [JsonProperty] public string GameType { get; private set; }
        public Game(Account player1, Account player2, GameType type) //Звичайна гра
        {
            FirstToMovePlayer = player1.NickName;
            SecondToMovePlayer = player2.NickName;
            Result = PlayGame();
            GameResult = Result.ToString();
            Rating = 5 + System.Math.Max(player1.CurrentRating,player2.CurrentRating)/5;
            GameType = type.ToString();
        }
        public Game(GameType type) //Перевизначення гри (гра проти бота)
        {
            GameType = type.ToString();
            FirstToMovePlayer = "X";
            SecondToMovePlayer = "O";
            Rating = 0;
            Result = PlayGameVsBot();
            GameResult = Result.ToString();
        }

        [JsonProperty] public string GameResult { get; private set; }

        [JsonConstructor]
        public Game() { }

    }
}
