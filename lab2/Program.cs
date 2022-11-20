using System;
using lab2.Games;
using lab2.GameAccounts;


namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
           
            VipAccount player1 = new VipAccount("Vova");
            PrivilegedAccount player2 = new PrivilegedAccount("Sanya");
            Console.WriteLine("Current rating of " + player1.UserName + ": " + player1.CurrentRating);
            Console.WriteLine("Current rating of " + player2.UserName + ": " + player2.CurrentRating);

            player1.LoseGame(player2, GameType.Standard);
            player2.WinGame(player1, GameType.Standard);
            player2.LoseGame(player1, GameType.Standard);
            player2.WinGame(player1, GameType.Standard);
            player1.WinGame(player2, GameType.Friendly);
            player1.LoseGame(player2, GameType.MaxRating);
            player2.WinGame(player1, GameType.Standard);
            player1.WinGame(player2, GameType.Friendly);
            player1.WinGame(player2, GameType.Standard);

            Console.WriteLine(player1.GetStats());
            Console.WriteLine(player2.GetStats());
            Console.WriteLine("Current rating of " + player1.UserName + ": " + player1.CurrentRating);
            Console.WriteLine("Current rating of " + player2.UserName + ": " + player2.CurrentRating);

            BaseAccount invalid = new BaseAccount("invalid");
            try
            {
                invalid.WinGame(invalid, GameType.Standard);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.ToString());
                return;
            }
        }
    }
}
