using System;

namespace lab2.GameAccounts
{
    //Віп аккаунт, рейтинг за перемогу подвоюється, за поразку ділиться на 2
    public class VipAccount : BaseAccount
    {
        public VipAccount(string userName) : base(userName) { }
        public override int CurrentRating { get
            {
                {
                    int countRating = 1;
                    foreach (var item in allGames)
                    {
                        if (this == item.Winner)
                        {
                            countRating += item.Rating*2;
                        }
                        else
                        {
                            countRating = Math.Max(1, countRating - (int)item.Rating / 2);
                        }
                    }

                    return countRating;
                }
            }
        }
    }
}
