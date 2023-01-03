using System;
using System.Collections.Generic;
using project.DataBase;

namespace project.UI
{
    class ManageController  
    {
        private List<IUserInterface> UIs { get; set; }
        public ManageController()
        {
            DBContext dBContext = new DBContext();
            UIs = new List<IUserInterface>
            {
                new AddUserController(dBContext),
                new SignInController(dBContext),
                new PlayGameController(dBContext)
            };
        }

        private void Show()
        {
            int actionNum;
            while (true)
            {
                Console.WriteLine(UIs[0].Message());
                actionNum = ActionNumber();
                switch (actionNum)
                {
                    case 1:
                        UIs[actionNum - 1].Action();
                        break;
                    case 2:
                        Console.Clear();
                        UIs[actionNum - 1].Action();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("-------------------------------------------------\n" +
                        "|  You entered wrong option. Please try again.  |\n" +
                        "-------------------------------------------------");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
            }
        }

        private int ActionNumber()
        {
            Console.WriteLine("------------------------\n|  Enter your option:  |\n------------------------");
            int value;
            try { value = int.Parse(Console.ReadLine());
               
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

        public void Run()
        {
         Show();
        }
    }
}

