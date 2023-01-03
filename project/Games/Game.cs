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
        [JsonIgnore] public Account FirstToMove { get; private set; }
        [JsonIgnore] public Account SecondToMove { get; private set; }
        [JsonProperty] public string FirstToMovePlayer { get; private set; }
        [JsonProperty] public string SecondToMovePlayer { get; private set; }
        [JsonIgnore] public PossibleResult Result { get; private set; }
        [JsonProperty] public int Rating { get; private set; }
        [JsonProperty] public string GameType { get; private set; }
        [JsonProperty] public string GameResult { get; private set; }

        public Game(Account player1, Account player2, GameType type) //Звичайна гра
        {
            FirstToMove = player1;
            SecondToMove = player2;
            FirstToMovePlayer = FirstToMove.NickName;
            SecondToMovePlayer = SecondToMove.NickName;
            Result = PlayGame();
            GameResult = Result.ToString();
            Rating = 5;
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

        [JsonConstructor]
        public Game() { }

    }
}
