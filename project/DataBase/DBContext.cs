using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using project.AccountFolder;
using project.Games;

namespace project.DataBase
{
    class DBContext
    {
        public string DBFilePath { get; private set; }
        public string DBFilePathToGamesHistory { get; private set; }
        public DBContext()
        {
            DBFilePath = @"D:\C# projects\project\DataBase\data.json";
            DBFilePathToGamesHistory = @"D:\C# projects\project\DataBase\games.json";
            if (File.Exists(DBFilePath) == false &&
                File.Exists(DBFilePathToGamesHistory) == false)
            {
                var file1 = File.Create(DBFilePath);
                var file2 = File.Create(DBFilePathToGamesHistory);
                file1.Close();
                file2.Close();
            }
        }
        public List<Game> GetAllGamesFromDB {  get
            {
                string json = File.ReadAllText(DBFilePathToGamesHistory);
                List<Game> allGames = JsonConvert.DeserializeObject<List<Game>>(json);
                return allGames ?? new List<Game>();
            } }
        public List<Account> GetAllAccountsFromDB { get
            {
                string json = File.ReadAllText(DBFilePath);
                List<Account> currentPlayers = JsonConvert.DeserializeObject<List<Account>>(json);
                return currentPlayers ?? new List<Account>();
            } }
    }
    }

