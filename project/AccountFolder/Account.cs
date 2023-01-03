using System;
using System.IO;
using Newtonsoft.Json;
using project.DataBase;
using project.Games;


namespace project.AccountFolder
{
    public class Account : IAccountInterface
    {
        [JsonProperty] public string UserID { get; private set; }
        [JsonProperty] public string NickName { get; private set; }
        [JsonProperty] public string UserPassword { get; private set; }
        public Account(string name, string password)
        {
            NickName = name;
            UserID = Guid.NewGuid().ToString();
            UserPassword = password;
        }

        public int CurrentRating
        {
            get
            {
                int value = 1;
                var gamesHistory = DBContext.ReadAllFromDBGames();
                foreach (var item in gamesHistory)
                {
                    if (item.FirstToMovePlayer == NickName || item.SecondToMovePlayer == NickName)
                    {
                        if (item.Result == PossibleResult.Draw)
                        {
                            value += 0;
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

                }
                return value;
            }
        }


        public void PlayGame(Account opponent) //Гра онлайн
        {
            if (opponent.NickName == NickName)
            {
                throw new ArgumentException();
            }
            Game game = new Game(this,opponent,GameType.online);
            var gamesHistory = DBContext.ReadAllFromDBGames();
            gamesHistory.Add(game);
            string serializedGames = JsonConvert.SerializeObject(gamesHistory);
            File.WriteAllText(DBContext.DBFilePathToGamesHistory, serializedGames);
        }

        public void PlayGame() //Гра з ботом
        {
            var game = new GameVsBot(GameType.training);
            var gamesHistory = DBContext.ReadAllFromDBGames();
            gamesHistory.Add(game);
            string serializedGames = JsonConvert.SerializeObject(gamesHistory);
            File.WriteAllText(DBContext.DBFilePathToGamesHistory, serializedGames);
        }

    }
}
