using System;
using project.DataBase;
using project.AccountFolder;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace project.UI
{
    class AddUserController : IUserInterface 
    {
        public DBContext Context { get; set; }
        public AddUserController(DBContext context)
        {
            Context = context;
        }
        public void Action()
        {
            Console.Clear();
            var allCurrentUsers = Context.GetAllAccountsFromDB;
            Console.WriteLine("-----------------------\n|  Enter a NickName:  |\n-----------------------");
            var name = Console.ReadLine();
            if (IsPlayerExist(name))
            {
                Console.WriteLine("------------------------------------------------------------\n" +
                    "|  This nickname is already taken. Please choose another.  |" +
                    "\n------------------------------------------------------------");
                System.Threading.Thread.Sleep(3000);
                Action();
                return;
            }
            Console.WriteLine("-----------------------\n|  Enter a PassWord:  |\n-----------------------");
            var password = Console.ReadLine();
            if (IsPasswordTaken(password))
            {
                Console.WriteLine("------------------------------------------------------------\n" +
                    "|  This password is already taken. Please choose another.  |\n" +
                    "------------------------------------------------------------");
                System.Threading.Thread.Sleep(3000);
                Action();
                return;
            }
            var HashedPW = Hashing.GetHashPassword(password);
            
            if (IsPasswordTaken(password))
            {
                Console.WriteLine("------------------------------------------------------------\n" +
                    "|  This password is already taken. Please choose another.  |\n" +
                    "------------------------------------------------------------");
                System.Threading.Thread.Sleep(3000);
                Action();
                return;
            }
            allCurrentUsers.Add(new Account(name,HashedPW));
            string serializedUsers = JsonConvert.SerializeObject(allCurrentUsers);
            File.WriteAllText(Context.DBFilePath, serializedUsers);
            Console.Clear();
        }
        public string Message()
        {
            return "----------------------------------------------\n" +
                "|            Welcome to Main Menu            |\n" +
                "----------------------------------------------\n" +
                "----------------------------------------------\n" +
                "|  1.Sign up  |  |  2.Sign in  |  |  3.Exit  |\n" +
                "----------------------------------------------";
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

        private bool IsPlayerExist(string name)
        {
            return Context.GetAllAccountsFromDB.FirstOrDefault(x => x.NickName == name) != null;
        }

        private bool IsPasswordTaken(string pass)
        {
            return Context.GetAllAccountsFromDB.FirstOrDefault(x => x.UserPassword == Hashing.GetHashPassword(pass)) != null;
        }
        
        
    }
}
