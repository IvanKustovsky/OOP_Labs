using System;
using System.Text;
using System.Threading;

namespace project.Games
{
    public enum PossibleResult
    {
        WinCrosses,
        WinNoughts,
        Draw
    }

    public enum FigureToPlay
    {
        crosses = 'X',
        noughts = 'O'
    }
    class GameLogic
    {
        public const int GRID_SIZE = 3;

        protected static char[,] board = { {' ', ' ', ' '},
                                        {' ', ' ', ' '},
                                        {' ', ' ', ' '}};

        private readonly char[,] instructionBoard = { {'1', '2', '3'},
                                        {'4', '5', '6'},
                                        {'7', '8', '9'}};

        private static int NumberOfMove = 0; //Кількість зроблених ходів
        
        protected PossibleResult PlayGame() //Звичайна гра
        {
            Console.WriteLine("---------------------------------------\n" +
                "|  You can see an instruction board.  |\n---------------------------------------");
            PrintBoard(instructionBoard);
            while (NumberOfMove < 9)
            {
                PlayerTurn();
                if (IsGameEnded())
                {
                    return DefineResult();
                }
                Console.WriteLine("-----------------------------\n|  The current board view:  |" +
                    "\n-----------------------------");
                PrintBoard(board);
            }
            Console.Clear();
            Console.WriteLine("-----------------------------\n|  " +
                "The game ended at draw.  |\n-----------------------------\n" +
                "\n------------------\n|  Final board:  |\n------------------");
            PrintBoard(board);
            Thread.Sleep(3000);
            CleanBoard();
            return PossibleResult.Draw;
        }
        protected PossibleResult PlayGameVsBot() //Гра проти комп'ютера
        {
            Console.WriteLine("---------------------------------------\n" +
                "|  You can see an instruction board.  |\n---------------------------------------");
            PrintBoard(instructionBoard);
            while (NumberOfMove < 9)
            {
                PlayerTurn();
                if (IsGameEnded())
                {
                    return DefineResult();
                }
                else if(IsBoardFilled() == true)
                {
                    Console.Clear();
                    Console.WriteLine("-----------------------------\n|  " +
                "The game ended at draw.  |\n-----------------------------\n" +
                "\n------------------\n|  Final board:  |\n------------------");
                    PrintBoard(board);
                    CleanBoard();
                    return PossibleResult.Draw;
                }
                ComputerTurn();
                if (IsGameEnded())
                {
                    return DefineResult();
                }
                Console.WriteLine("-----------------------------\n|  The current board view:  |" +
                    "\n-----------------------------");
                PrintBoard(board);
            }
            return PossibleResult.Draw;
        }
        private void PlayerTurn() //Хід гравця
        {
            int num;
            int[] coords;
            Console.WriteLine("------------------------------------------------------\n" +
                "|  Enter the number of cell (1-9) you want to play:  |\n------------------------------------------------------");
                num = ActionNumber();
                    if (IsValidPlacement(num))
                    {
                    coords = CoordinatesByNumber(num);
                    board[coords[0], coords[1]] = (NumberOfMove % 2 == 0) ? 'X' : 'O';
                    NumberOfMove++;
                    }

                    else
                    {
                        Console.WriteLine("------------------------------------------------------------------------------\n|  " +
                            "You entered the number of cell that is already filled. Please try again.  |\n" +
                            "------------------------------------------------------------------------------");
                PrintBoard(board);
                PlayerTurn();
                    }

        } 
        private void ComputerTurn() //Хід комп'ютера
        {
            if(IsGameEnded() == false && NumberOfMove == 9)
            {
                return;
            }
            Random rand = new Random();
            int computerMove;
            int[] coords;
            while (true)
            {
                computerMove = rand.Next(1, 10);
                if (IsValidPlacement(computerMove))
                {
                    coords = CoordinatesByNumber(computerMove);
                    board[coords[0], coords[1]] = (NumberOfMove % 2 == 0) ? 'X' : 'O';
                    NumberOfMove++;
                    Console.Write("---------------------------------\n|  Computer has chosen cell: " +
                         computerMove + "  |\n---------------------------------\n");
                    break;
                }
            }
        }
        private bool IsGameEnded() //Перевірка чи є результат гри
        {
            char figureToCheck = (NumberOfMove % 2 == 1) ? 'X' : 'O';
            if ((board[0, 0] == figureToCheck && board[0, 1] == figureToCheck && board[0, 2] == figureToCheck) ||
                (board[1, 0] == figureToCheck && board[1, 1] == figureToCheck && board[1, 2] == figureToCheck) ||
                (board[2, 0] == figureToCheck && board[2, 1] == figureToCheck && board[2, 2] == figureToCheck) ||
                (board[0, 0] == figureToCheck && board[1, 0] == figureToCheck && board[2, 0] == figureToCheck) ||
                (board[0, 1] == figureToCheck && board[1, 1] == figureToCheck && board[2, 1] == figureToCheck) ||
                (board[0, 2] == figureToCheck && board[1, 2] == figureToCheck && board[2, 2] == figureToCheck) ||
                (board[0, 0] == figureToCheck && board[1, 1] == figureToCheck && board[2, 2] == figureToCheck) ||
                (board[0, 2] == figureToCheck && board[1, 1] == figureToCheck && board[2, 0] == figureToCheck))
            {
                return true;
            }
            return false;
        }
        private bool IsBoardFilled()// Перевірка чи дошка заповнена
        {
            int[] coords;
            for (int i = 1; i <= 9; i++)
            {
                coords = CoordinatesByNumber(i);
                if(board[coords[0], coords[1]] == ' ')
                {
                    return false;
                }
            }
            return true;
        }
        private PossibleResult DefineResult() {
            char WinnerChar = (NumberOfMove % 2 == 1) ? 'X' : 'O';
            Console.WriteLine("----------------------------\n|  The winner is  " +
                (FigureToPlay)WinnerChar + "  |\n----------------------------" +
               "\n------------------\n|  Final board:  |\n------------------");
            PossibleResult result = ((FigureToPlay)WinnerChar == FigureToPlay.crosses) ?
                    PossibleResult.WinCrosses : PossibleResult.WinNoughts;
            PrintBoard(board);
            CleanBoard();
            return result;
        }
        private bool IsValidNumber(int n) //Перевірка чи число знаходить в діапазоні (1-9)
        {
            return (n > 0 && n < 10);
        }
        private bool IsValidPlacement(int n) //Перевірка чи доступне місця для заповнення дошки
        {
            if (IsValidNumber(n))
            {
                int[] coords = CoordinatesByNumber(n);
                if (board[coords[0], coords[1]] == ' ')
                {
                    return true;
                }
            }
            return false;
        } 
        private int[] CoordinatesByNumber(int n) //Обчислити координати дошки за номером (1-9)
        {
            if (IsValidNumber(n))
            {
                int counter = 0;
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        if (counter == n - 1)
                        {
                            return new int[] { i, j };
                        }
                        counter++;
                    }
                }
            }
            return null;
        } 
        private void PrintBoard(char[,] boardToPrint) //Вивести дошку на екран
        {
            string toPrint;
            StringBuilder build = new StringBuilder();
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    toPrint = j != 2 ? (" " + boardToPrint[i, j] + " |") : (" " + boardToPrint[i, j].ToString());
                    build.Append(toPrint);
                }
                Console.WriteLine(build + "\n-----------");
                build.Clear();
            }
        }
        private void CleanBoard() //Очистити дошку
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    board[i, j] = ' ';
                }
            }
            NumberOfMove = 0;
        } 
        private int ActionNumber() //Номер вводу
        {
            Console.WriteLine("------------------------\n|  Enter your choice:  |\n------------------------");
            int value;
            try
            {
                value = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------------------------" +
                    "\n|  You entered wrong value of input. Please try again.  |" +
                    "\n---------------------------------------------------------");
                PrintBoard(board);
                value = ActionNumber();
            }
            return value;
        }

    }
}

