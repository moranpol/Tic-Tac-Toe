using System;
using System.Diagnostics.Eventing.Reader;

namespace XMixDrixUpsideDown
{
    public struct UI
    {
        public static string GetUserName(int i_UserNum)
        {
            Console.WriteLine("Please write your user #" + i_UserNum + " name:");
            string v_Name = Console.ReadLine();

            while (v_Name == "")
            {
                Console.WriteLine("Wrong input, please write your user name:");
                v_Name = Console.ReadLine();
            }

            return v_Name;
        }

        public static bool IsUser2Computer() 
        {
            Console.WriteLine("Do you want to play against the computer? enter Y/N");
            return checkIfInputYOrN();
        }

        public static int GetTableSize()
        {
            Console.WriteLine("Enter game table size. between 3-9:");
            string v_StrSize = Console.ReadLine();
            int v_Size;
            while(!(int.TryParse(v_StrSize, out v_Size) && !(v_Size < 3 || v_Size > 9)))
            {
                Console.WriteLine("Wrong input, please write number between 3-9:");
                v_StrSize = Console.ReadLine();
            }

            return v_Size;
        }

        public static Tuple<int, int> GetCellFromUser(string i_Name, int i_TableSize)
        {
            Tuple<int, int> v_Cell = null;
            int v_RowNum = 0, v_ColNum = 0;
            string v_StrCell;
            string[] v_SplitAnsStr;

            Console.WriteLine(i_Name + " please select cell: ");
            Console.WriteLine("Enter row, column number between 1-" + i_TableSize + ". Numbers split by space.");
            v_StrCell = Console.ReadLine();
            v_SplitAnsStr = v_StrCell.Split(' ');

            while(!checkIfQuit(v_StrCell) && !checkIsValidCell(v_SplitAnsStr, i_TableSize, out v_RowNum, out v_ColNum))
            {
                Console.WriteLine("Wrong input, Enter row, column number between 1-" + i_TableSize + ". Numbers split by space.");
                v_StrCell = Console.ReadLine();
                v_SplitAnsStr = v_StrCell.Split(' ');
            }

            if(!checkIfQuit(v_StrCell))
            {
                v_Cell = new Tuple<int, int>(v_RowNum, v_ColNum);
            }

            return v_Cell;
        }

        public static void PrintTable(eSigns[,] i_GameTable, int i_TableSize)
        {
            Console.Write("  ");
            for (int i = 1; i <= i_TableSize; i++) 
            { 
                Console.Write(" " + i + "  ");
            }
            Console.WriteLine();
            for (int i = 0; i < i_TableSize; i++)
            {
                Console.Write((i + 1) + "|");
                for (int j = 0; j < i_TableSize; j++)
                {
                    if (i_GameTable[i, j]==eSigns.Empty)
                    {
                        Console.Write("   |");
                    }
                    else if (i_GameTable[i, j] == eSigns.O)
                    {
                        Console.Write(" O |");
                    }
                    else
                    {
                        Console.Write(" X |");
                    }
                }
                Console.WriteLine();
                Console.Write(" =");
                for (int j = 0; j < i_TableSize; j++)
                {
                    Console.Write("====");
                }
                Console.WriteLine();
            }
        }

        private static bool checkIfQuit(string i_Str)
        {
            // $G$ NTT-999 (-7) You should have use char.ToUpper / char.ToLower here.
            // $G$ CSS-999 (-5) You should have use constant here instead of "Q".
            return i_Str == "Q";
        }

        private static bool checkIsValidCell(string[] i_CellStrArray, int i_TableSize, out int o_Row, out int o_Col)
        {
            bool v_IsValidCell = true;
            o_Row = 0;
            o_Col = 0;

            if (i_CellStrArray.Length != 2)
            {
                v_IsValidCell = false;
            }
            else if (!(int.TryParse(i_CellStrArray[0], out o_Row) && int.TryParse(i_CellStrArray[1], out o_Col)))
            {
                v_IsValidCell = false;
            }
            else if((o_Row < 1 || o_Row > i_TableSize) || (o_Col < 1 || o_Col > i_TableSize))
            {
                v_IsValidCell = false;
            }

            return v_IsValidCell;
        }

        public static void PrintNotAvailableCell()
        {
            Console.WriteLine("Cell isn't available");
        }

        public static void PrintWinner(string i_Name)
        {
            Console.WriteLine(i_Name + " is the winner!");
        }

        public static void PrintScoreBoard(User i_User1, User i_User2)
        {
            Console.WriteLine("Score Board:");
            Console.WriteLine(i_User1.Name + " score is " + i_User1.Score);
            Console.WriteLine(i_User2.Name + " score is " + i_User2.Score);
        }

        public static void PrintTie()
        {
            Console.WriteLine("Tie!!!!!");
        }

        public static bool CheckIfStartNewGame()
        {
            Console.WriteLine("Do you want to play another game? enter Y/N");
            return checkIfInputYOrN();
        }

        private static bool checkIfInputYOrN() 
        {
            string v_Input = Console.ReadLine();
            // $G$ NTT-999 (0) You should have use char.ToUpper / char.ToLower here.
            while (v_Input != "Y" && v_Input != "N")
            {
                Console.WriteLine("Wrong input, please write Y/N:");
                v_Input = Console.ReadLine();
            }

            return v_Input == "Y";
        }
    }
}
