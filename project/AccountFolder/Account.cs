using System;
using System.IO;
using Newtonsoft.Json;
using project.DataBase;
using project.Games;

namespace project.AccountFolder
{
    public class Account : IAccountInterface
    {
        [JsonProperty]public string UserID { get; private set; }
        [JsonProperty] public string NickName { get; private set; }
        [JsonProperty] public string UserPassword { get; private set; }
        public Account(string name, string password)
        {
            NickName = name;
            UserID = Guid.NewGuid().ToString();
            UserPassword = password;
        }
        private readonly DBContext Context = new DBContext();
        public int CurrentRating
        {
            get
            {
                int value = 1;
                var gamesHistory = Context.GetAllGamesFromDB;
                foreach (var item in gamesHistory)
                {
                    if ((item.FirstToMovePlayer != NickName && item.SecondToMovePlayer != NickName) 
                        || item.Result == PossibleResult.Draw) { //Якщо гра не містить гравця для якого перевіряємо, або нічия
                     continue; 
                    }

                        if ((NickName == item.FirstToMovePlayer && item.Result == PossibleResult.WinCrosses) ||
                            (NickName == item.SecondToMovePlayer && item.Result == PossibleResult.WinNoughts))
                        {
                            value += item.Rating;
                        }
                        else
                        {
                            value = Math.Max(1, value - item.Rating);
                        }
                }
                return value;
            }
        }
        public void PlayGame(Account opponent) //Гра онлайн
        {
            if (opponent == null || opponent.NickName == NickName)
            {
                throw new ArgumentException();
            }
            Game game = new Game(this,opponent,GameType.online);
            var gamesHistory = Context.GetAllGamesFromDB;
            gamesHistory.Add(game);
            string serializedGames = JsonConvert.SerializeObject(gamesHistory);
            File.WriteAllText(Context.DBFilePathToGamesHistory, serializedGames);
        }
        public void PlayGame() //Гра з ботом
        {
            var game = new Game(GameType.training);
            var gamesHistory = Context.GetAllGamesFromDB;
            gamesHistory.Add(game);
            string serializedGames = JsonConvert.SerializeObject(gamesHistory);
            File.WriteAllText(Context.DBFilePathToGamesHistory, serializedGames);
        }
    }
}
