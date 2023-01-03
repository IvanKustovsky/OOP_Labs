using project.DataBase;
using project.AccountFolder;
using System;
using Newtonsoft.Json;
using System.IO;

namespace project.UI
{
    class PlayGameController : IUserInterface
    {
        public DBContext Context { get; private set; }
        public SignInController Temp { get; private set; }
        public PlayGameController(DBContext context)
        {
            Context = context;
            Temp = new SignInController(context);
        }
        public void Action()
        {
                Console.WriteLine(Message());
                int res = ActionNumber();
                Console.Clear();
                switch (res)
                {
                    case 1:
                    SignInController.CurrentPlayer.PlayGame(); //Гра проти бота
                    Action();
                    break;
                    case 2:
                    ActionCase2(); //Гра онлайн
                    Action();
                    break;
                    case 3:
                    Temp.ActionCases(); //Повернення до минулого меню
                    break;
                    case 4:
                        Environment.Exit(0); //Вихід з програми
                        break;
                    default:
                    Console.WriteLine("-------------------------------------------------\n" +
                    "|  You entered wrong option. Please try again.  |\n" +
                    "-------------------------------------------------");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Action();
                    break;
                }
        }

        private void ActionCase2()
        {
            Console.Clear();
            Console.WriteLine("---------------------------\n|  Choose your opponent:  |" +
                "\n---------------------------");
            Temp.SignIn();
            try
            {
                SignInController.CurrentPlayer.PlayGame(Temp.Opponent); //Перевірка чи гра не проти себе
            }
            catch (ArgumentException)
            {
                Console.WriteLine("--------------------------------------\n" +
                    "|  You can't play against yourself.  |\n--------------------------------------");
                Console.WriteLine("--------------------------------------\n" +
                    "|  Do you want to try again? (Y/N).  |" +
                    "\n--------------------------------------");
                if (OptionChoose())
                {
                    ActionCase2();
                    return;
                }
                Action();
                return;
            }
            //Оновлюємо рейтинг гравців, що грали
            string json = File.ReadAllText(DBContext.DBFilePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            jsonObj[GetIndexOfPlayer(SignInController.CurrentPlayer)]["CurrentRating"] = SignInController.CurrentPlayer.CurrentRating;
            jsonObj[GetIndexOfPlayer(Temp.Opponent)]["CurrentRating"] = Temp.Opponent.CurrentRating;
            string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(DBContext.DBFilePath, output);
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
            if (value.ToLower() == "y")
            {
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

        private int GetIndexOfPlayer(Account user)
        {
            return DBContext.ReadAllFromDB().FindIndex(x => x.NickName == user.NickName);
        }

        public string Message()
        {
            return "------------------------------------------------------------------------------\n" +
               "|  1.Training  |  |  2.Online  |  |  3.Back to the previous menu  |  4.Exit  |\n" +
               "------------------------------------------------------------------------------";
        }
    }
}
