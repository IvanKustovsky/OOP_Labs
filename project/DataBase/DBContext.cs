using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using project.AccountFolder;
using project.Games;

namespace project.DataBase
{
    class DBContext
    {
        //Введіть ваш шлях до файлу
        public static string DBFilePath { get; } = @"D:\C# projects\project\DataBase\data.json";
        public static string DBFilePathToGamesHistory { get; } = @"D:\C# projects\project\DataBase\games.json";
        public DBContext()
        {
            if (File.Exists(DBFilePath) == false  && File.Exists(DBFilePathToGamesHistory) == false)
            {
                var file1 = File.Create(DBFilePath);
                var file2 = File.Create(DBFilePathToGamesHistory);
                file1.Close();
                file2.Close();
            }
        }
       
        public static List<Game> ReadAllFromDBGames()
        {
            string json = File.ReadAllText(DBFilePathToGamesHistory);
            List<Game> allGames = JsonConvert.DeserializeObject<List<Game>>(json);
            return allGames ?? new List<Game>();
        }

        public static List<Account> ReadAllFromDB()
        {
            string json = File.ReadAllText(DBFilePath);
            List<Account> currentPlayers = JsonConvert.DeserializeObject<List<Account>>(json);
            return currentPlayers ?? new List<Account>();
        }

    }
    }

