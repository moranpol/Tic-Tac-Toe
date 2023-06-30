using System;
using System.Collections.Generic;

namespace Logic
{
    internal enum eSigns
    {
        Empty,
        X,
        O,
    }

    public class GameTable
    {
        private const int v_NumOfDiagonalLines = 2;

        private eSigns[,] m_Table;
        private int m_SizeOfTable;
        private int m_Capacity;
        private LineDetails[] m_RowsDetails;
        private LineDetails[] m_ColsDetails;
        private LineDetails[] m_DiagonalLineDetails;

        public int SizeOfTable
        {
            get
            {
                return m_SizeOfTable;
            }
            set
            {
                m_SizeOfTable = value;
            }
        }

        internal eSigns[,] Table
        {
            get
            {
                return m_Table;
            }
        }

        internal bool CheckIfLoose(int i_Row, int i_Col)
        {
            bool v_IsLoose = false;
            if (m_RowsDetails[i_Row].IsOneSignInLine && m_RowsDetails[i_Row].CountSign == m_SizeOfTable)
            {
                v_IsLoose = true;
            }
            else if (m_ColsDetails[i_Col].IsOneSignInLine && m_ColsDetails[i_Col].CountSign == m_SizeOfTable)
            {
                v_IsLoose = true;
            }
            else if (isInLeftDiagonalLine(i_Row, i_Col) && m_DiagonalLineDetails[0].IsOneSignInLine && m_DiagonalLineDetails[0].CountSign == m_SizeOfTable)
            {
                v_IsLoose = true;
            }
            else if (isInRightDiagonalLine(i_Row, i_Col) && m_DiagonalLineDetails[1].IsOneSignInLine && m_DiagonalLineDetails[1].CountSign == m_SizeOfTable)
            {
                v_IsLoose = true;
            }
            return v_IsLoose;
        }

        internal bool CheckIfTie()
        {
            return m_Capacity == m_SizeOfTable * m_SizeOfTable;
        }

        internal bool IsAvailableCell(int i_Row, int i_Col)
        {
            return m_Table[i_Row, i_Col] == eSigns.Empty;            
        }

        internal void InsertNewSign(eSigns i_NewSign, int i_Row, int i_Col)
        {
            m_Table[i_Row, i_Col] = i_NewSign;
            updateLinesDetails(i_NewSign, i_Row, i_Col);
            m_Capacity++;
        }

        private void updateLinesDetails(eSigns i_NewSign, int i_Row, int i_Col)
        {
            m_RowsDetails[i_Row].UpdateLineDetails(i_NewSign);
            m_ColsDetails[i_Col].UpdateLineDetails(i_NewSign);
            if(isInLeftDiagonalLine(i_Row, i_Col))
            {
                m_DiagonalLineDetails[0].UpdateLineDetails(i_NewSign);
            }
            if(isInRightDiagonalLine(i_Row, i_Col))
            {
                m_DiagonalLineDetails[1].UpdateLineDetails(i_NewSign);
            }
        }

        private bool isInLeftDiagonalLine(int i_Row, int i_Col)
        {
            return i_Row == i_Col;
        }

        private bool isInRightDiagonalLine(int i_Row, int i_Col)
        {
            return i_Row + i_Col == m_SizeOfTable - 1;
        }

        internal void RestartInformationInTable()
        {
            m_Table = new eSigns[m_SizeOfTable, m_SizeOfTable];
            m_Capacity = 0;
            m_RowsDetails = new LineDetails[m_SizeOfTable];
            m_ColsDetails = new LineDetails[m_SizeOfTable];
            for(int i = 0; i < m_SizeOfTable; i++)
            {
                m_RowsDetails[i] = new LineDetails();
                m_ColsDetails[i] = new LineDetails();
            }

            m_DiagonalLineDetails = new LineDetails[v_NumOfDiagonalLines];
            for (int i = 0; i < v_NumOfDiagonalLines; i++)
            {
                m_DiagonalLineDetails[i] = new LineDetails();
            }
        }

        internal Tuple<int, int> ChooseRandomCellForComputerUser()
        {
            List<Tuple<int, int>> v_AvailableCells = new List<Tuple<int, int>>();
            Random v_Rand = new Random();
            int v_Index;

            for (int i = 0; i < m_SizeOfTable; i++)
            {
                for (int j = 0; j < m_SizeOfTable; j++)
                {
                    if (IsAvailableCell(i, j))
                    {
                        v_AvailableCells.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            v_Index = v_Rand.Next(v_AvailableCells.Count);
            return v_AvailableCells[v_Index];
        }
    }
}
