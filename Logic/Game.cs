using System;
using System.Runtime.CompilerServices;

namespace Logic
{
    public enum ePlayerTurn
    {
        PlayerO, PlayerX
    }

    public enum eGameStatus
    {
        Tie, Win, Continue
    }

    public class Game
    {
        private const int v_NumOfUsers = 2;

        private User[] m_Users;
        private GameTable m_Table;
        private ePlayerTurn m_PlayerTurn;

        public GameTable Table { get { return m_Table; } }

        public User[] Users { get { return m_Users; } }

        public ePlayerTurn PlayerTurn { get { return m_PlayerTurn; } set { m_PlayerTurn = value; } }

        public Game() 
        {
            m_Users = new User[v_NumOfUsers];
            m_Table = new GameTable();
            m_PlayerTurn = ePlayerTurn.PlayerO;
        }

        public void CreateUser(int i_UserNum, bool i_IsComputer, string i_UserName)
        {
            m_Users[i_UserNum] = new User();
            m_Users[i_UserNum].Name = i_UserName;
            m_Users[i_UserNum].IsComputer = i_IsComputer;
        }

        public eGameStatus GameRound(Tuple<int, int> i_ChosenCell)
        {
            eGameStatus v_GameStatus = eGameStatus.Continue;

            m_Table.InsertNewSign(returnUserSign(m_PlayerTurn), i_ChosenCell.Item1, i_ChosenCell.Item2);
            if (m_Table.CheckIfLoose(i_ChosenCell.Item1, i_ChosenCell.Item2))
            {
                m_Users[returnNextUserIndex()].UpdateScore();
                v_GameStatus = eGameStatus.Win;
            }
            else if(m_Table.CheckIfTie())
            {
                v_GameStatus = eGameStatus.Tie;
            }

            m_PlayerTurn = returnNextUserTurn();

            return v_GameStatus;
        }

        public int ReturnCurrentUserIndex()
        {
            int v_UserIndex = 0;

            if (m_PlayerTurn == ePlayerTurn.PlayerX)
            {
                v_UserIndex = 1;
            }

            return v_UserIndex;
        }

        private eSigns returnUserSign(ePlayerTurn i_PlayerTurn)
        {
            eSigns v_ResSign = eSigns.O;

            if(i_PlayerTurn == ePlayerTurn.PlayerX)
            {
                v_ResSign = eSigns.X;
            }

            return v_ResSign;
        }

        private int returnNextUserIndex()
        {
            int v_NextPlayer = 0;

            if (m_PlayerTurn == ePlayerTurn.PlayerO)
            {
                v_NextPlayer = 1;
            }

            return v_NextPlayer;
        }

        private ePlayerTurn returnNextUserTurn()
        {
            ePlayerTurn v_NextPlayer = ePlayerTurn.PlayerO;

            if (m_PlayerTurn == ePlayerTurn.PlayerO)
            {
                v_NextPlayer = ePlayerTurn.PlayerX;
            }

            return v_NextPlayer;
        }
    }
}
