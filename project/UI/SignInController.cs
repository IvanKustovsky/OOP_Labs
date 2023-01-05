using project.AccountFolder;
using project.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project.UI
{
    class SignInController : IUserInterface
    {
        public DBContext Context { get; private set; }
        public static Account CurrentPlayer { get; private set; } //Аккаунт, в який увійшли
        public Account Opponent { get; private set; } //Аккаунт опонента
        public SignInController(DBContext context)
        {
            Context = context;
        }
        public void SignIn() //Вхід в аккаунт(підтвердження)
        {
            Console.WriteLine("---------------------------\n|  Welcome to Login Menu  |\n---------------------------");
            Console.WriteLine("---------------------\n|  Enter NickName:  |\n---------------------");
            var name = Console.ReadLine();
            if (!IsPlayerExist(name)) {
                Console.WriteLine("-----------------------------------------------------------------------------\n" +
                    "|  There is no player with this nickname. Do you want to try again? (Y/N).  |" +
                    "\n-----------------------------------------------------------------------------");
                if (OptionChoose())
                {
                    Console.Clear();
                    SignIn();
                    return;
                }
                return;
            }
            Console.WriteLine("---------------------\n|  Enter Password:  |\n---------------------");
            var password = Console.ReadLine();
            if (!IsCorrectPassword(name, Hashing.GetHashPassword(password))) {
                Console.WriteLine("-----------------------------------------------------------------\n" +
                    "|  The password is incorrect. Do you want to try again? (Y/N).  |" +
                    "\n-----------------------------------------------------------------");
                if (OptionChoose())
                {
                    Console.Clear();
                    SignIn();
                    return;
                }
                return;
            }

            if(CurrentPlayer == null) { CurrentPlayer = GetPlayer(name); }
            else
            {
                Opponent = GetPlayer(name);
            }
            Console.Clear();
        }
        public void Action()
        {
            if(CurrentPlayer == null) { SignIn(); }
            if (CurrentPlayer != null) { ActionCases(); }
        }
        public string Message()
        {
            return "----------------------------------------------------------------------\n" +
                "|  1.Log out  |  |  2.Play Game  |  |  3.Show History  |  |  4.Exit  |\n" +
                "----------------------------------------------------------------------";
        }
        public void ActionCases()
        {
            Console.WriteLine(Message());
            int option = ActionNumber();
            Console.Clear();
            switch (option)
            {
                case 1:
                    CurrentPlayer = null;
                    Console.Clear();
                    break;
                case 2:
                    new PlayGameController(Context).Action();
                    break;
                case 3:
                    Console.WriteLine(GetStats());
                    Action();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("-------------------------------------------------\n" +
                        "|  You entered wrong option. Please try again.  |\n" +
                        "-------------------------------------------------");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    ActionCases();
                    break;
            }
        }

        private string GetStats()
        {
            StringBuilder str = new StringBuilder();
            var gamesHistory = Context.GetAllGamesFromDB;
            string opponent;
            string result;
            str.AppendLine("-----------------------------------------------\n" +
                "|  Opponent  |  |  Game Rating  |  |  Result  |\n-----------------------------------------------");
            foreach (var item in gamesHistory)
            { if (item.FirstToMovePlayer == CurrentPlayer.NickName || item.SecondToMovePlayer == CurrentPlayer.NickName)
                {
                    result = "Draw";
                     opponent = CurrentPlayer.NickName == item.FirstToMovePlayer 
                        ? item.SecondToMovePlayer : item.FirstToMovePlayer;
                    if(item.Result != Games.PossibleResult.Draw) {
                        result = (CurrentPlayer.NickName == item.FirstToMovePlayer
                        && item.Result == Games.PossibleResult.WinCrosses) ||
                        (CurrentPlayer.NickName == item.SecondToMovePlayer && 
                        item.Result == Games.PossibleResult.WinNoughts) ? "Victory" : "Defeat";
                    }
                    str.AppendLine("\t" + opponent + "\t\t" + item.Rating + "\t\t" + result);
                } 
            }
            str.AppendLine("Current rating = " + CurrentPlayer.CurrentRating);
            return str.ToString();
        }
        private int ActionNumber()
        {
            Console.WriteLine("------------------------\n|  Enter your option:  |\n------------------------");
            int value;
            try
            {
                value = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("---------------------------------------------------------" +
                    "\n|  You entered wrong value of input. Please try again.  |" +
                    "\n---------------------------------------------------------");
                value = ActionNumber();
            }
            return value;
        }
        private bool OptionChoose()
        {
            Console.WriteLine("------------------------\n|  Enter your option:  |\n------------------------");
            string value = Console.ReadLine();
            if(value.ToLower() == "y") {
                Console.Clear();
                return true;
            }
            else if (value.ToLower() == "n")
            {
                Console.Clear();
                return false;
            }
            Console.WriteLine("-------------------------------------------------------------------------" +
                     "\n|  You entered wrong value of input. You should enter Y(yes) or N(no).  |" +
                     "\n-------------------------------------------------------------------------");
            return OptionChoose();
        }
        private Account GetPlayer(string name)
        {
            List<Account> allCurrentUsers = Context.GetAllAccountsFromDB;
            return allCurrentUsers.First(x => x.NickName == name);
        }
        private bool IsPlayerExist(string name)
        {
            return Context.GetAllAccountsFromDB.FirstOrDefault(x => x.NickName == name) != null;
        }
        private bool IsCorrectPassword(string name, string password)
        {
            return IsPlayerExist(name) && Context.GetAllAccountsFromDB.First(x => x.NickName == name).UserPassword == password;
        }

        
    }
}
