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
        [JsonIgnore] public Account FirstToMove { get; protected set; }
        [JsonIgnore] public Account SecondToMove { get; protected set; }
        [JsonProperty] public string FirstToMovePlayer { get; protected set; }
        [JsonProperty] public string SecondToMovePlayer { get; protected set; }
        [JsonIgnore] public PossibleResult Result { get; protected set; }
        [JsonProperty] public int Rating { get; protected set; }
        [JsonProperty] public string GameType { get; protected set; }
        [JsonProperty] public string GameResult { get; protected set; }

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

        [JsonConstructor]
        public Game() { }

    }
}
