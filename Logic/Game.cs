using System;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Game
    {
        private const int v_NumOfUsers = 2;

        private User[] m_Users;
        private GameTable m_Table;

        public GameTable Table { get { return m_Table; } }

        public Game() 
        {
            m_Users = new User[v_NumOfUsers];
            m_Table = new GameTable();
        }

        public void CreateUser(int i_UserNum, bool i_IsComputer, string i_UserName)
        {
            m_Users[i_UserNum] = new User();
            m_Users[i_UserNum].Name = i_UserName;
            m_Users[i_UserNum].IsComputer = i_IsComputer;
        }

        public void ManageGame()
        {
            bool v_ContinueGame = true;

            //CreateUser(0, false);
            //m_Table.SizeOfTable = UI.GetTableSize();
            //m_Table.RestartInformationInTable();
            //(1, UI.IsUser2Computer());

            while(v_ContinueGame)
            {
                gameRound();
                UI.PrintScoreBoard(m_Users[0], m_Users[1]);
                v_ContinueGame = UI.CheckIfStartNewGame();
                Ex02.ConsoleUtils.Screen.Clear();
                m_Table.RestartInformationInTable();
            }
        }

        private void gameRound()
        {
            int v_UserIndex = 0;
            Tuple<int, int> v_ChosenCell;
            bool v_ContinueGameRound = true;
            UI.PrintTable(m_Table.Table, m_Table.SizeOfTable);

            while (v_ContinueGameRound)
            {
                v_ChosenCell = null;
                if(m_Users[v_UserIndex].IsComputer)
                {
                    v_ChosenCell = m_Table.ChooseRandomCellForComputerUser();
                }
                else
                {
                    v_ChosenCell = UI.GetCellFromUser(m_Users[v_UserIndex].Name, m_Table.SizeOfTable);
                    while (v_ChosenCell != null && !m_Table.IsAvailableCell(v_ChosenCell.Item1 - 1, v_ChosenCell.Item2 - 1))
                    {
                        UI.PrintNotAvailableCell();
                        v_ChosenCell = UI.GetCellFromUser(m_Users[v_UserIndex].Name, m_Table.SizeOfTable);
                    }
                }

                if(v_ChosenCell == null) 
                {
                    m_Users[returnNextUserIndex(v_UserIndex)].UpdateScore();
                    break;
                }
                else if(!m_Users[v_UserIndex].IsComputer)
                {
                    v_ChosenCell = new Tuple<int, int>(v_ChosenCell.Item1 - 1, v_ChosenCell.Item2 - 1);
                }

                m_Table.InsertNewSign(returnUserSign(v_UserIndex), v_ChosenCell.Item1, v_ChosenCell.Item2);
                Ex02.ConsoleUtils.Screen.Clear();
                UI.PrintTable(m_Table.Table, m_Table.SizeOfTable);

                if (m_Table.CheckIfLoose(v_ChosenCell.Item1, v_ChosenCell.Item2))
                {
                    UI.PrintWinner(m_Users[returnNextUserIndex(v_UserIndex)].Name);
                    m_Users[returnNextUserIndex(v_UserIndex)].UpdateScore();
                    v_ContinueGameRound = false;
                }
                else if(m_Table.CheckIfTie())
                {
                    UI.PrintTie();
                    v_ContinueGameRound = false;
                }

                v_UserIndex = returnNextUserIndex(v_UserIndex);
            }
        }

        private eSigns returnUserSign(int i_UserIndex)
        {
            eSigns v_ResSign = eSigns.O;
            if(i_UserIndex == 0)
            {
                v_ResSign = eSigns.X;
            }

            return v_ResSign;
        }

        private int returnNextUserIndex(int i_CurrUserIndex)
        {
            return (i_CurrUserIndex + 1) % v_NumOfUsers;
        }
    }
}
