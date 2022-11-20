using lab2.Games;
using System;

namespace lab2.GameAccounts
{
    //Аккаунт, в якому за серію перемог(>=3) рейтинг, на який була гра подвоюється.
    public class PrivilegedAccount : BaseAccount
    {
        public PrivilegedAccount(string name) : base(name) { }

        public override int CurrentRating
        {
            get
            {
                {
                    int countRating = 1;
                    int gameWonInRow = 0;
                    foreach (var item in allGames)
                    {
                        if (this == item.Winner)
                        {
                            gameWonInRow++;
                            countRating += (gameWonInRow>3) ? item.Rating*2 : item.Rating;
                        }
                        else
                        {
                            countRating = Math.Max(1, countRating - item.Rating);
                            gameWonInRow = (item.Type == GameType.Friendly) ? gameWonInRow : 0;
                        }
                    }
                    return countRating;
                }
            }
        }

    }
}
