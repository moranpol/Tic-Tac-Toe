using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace UI
{
    public partial class TicTacToeMisereForm : Form
    {
        private const int k_ButtonSize = 50;
        private const int k_Spacing = 5;
        private const int k_WidthDesign = 15;
        private const int k_HeightDesign = 70;
        private const string k_O = "O";
        private const string k_X = "X";

        private Game m_Game;
        private Button[,] m_ButtonsArr;

        public TicTacToeMisereForm(Game i_Game)
        {
            m_Game = i_Game;
            m_ButtonsArr = new Button[m_Game.Table.SizeOfTable, m_Game.Table.SizeOfTable];

            InitializeComponent();
            createTable();
            showScoreLabel();
        }

        private void createTable()
        {
            int v_SizeOfTable = m_Game.Table.SizeOfTable;

            int v_FormWidth = v_SizeOfTable * (k_ButtonSize + k_Spacing) + k_Spacing + k_WidthDesign;
            int v_FormHeight = v_SizeOfTable * (k_ButtonSize + k_Spacing) + k_Spacing + k_HeightDesign;
            Size = new Size(v_FormWidth, v_FormHeight);
            MaximumSize = Size;
            MinimumSize = Size;

            for (int v_Row = 0; v_Row < v_SizeOfTable; v_Row++)
            {
                for (int v_Col = 0; v_Col < v_SizeOfTable; v_Col++)
                {
                    Button v_Button = new Button();
                    v_Button.Size = new Size(k_ButtonSize, k_ButtonSize);
                    v_Button.Location = new Point(
                        k_Spacing + v_Col * (k_ButtonSize + k_Spacing),
                        k_Spacing + v_Row * (k_ButtonSize + k_Spacing));
                    v_Button.Name = string.Format("{0}_{1}", v_Row, v_Col);
                    m_ButtonsArr[v_Row, v_Col] = v_Button;
                    v_Button.Click += button_Click;
                    Controls.Add(v_Button);
                }
            }
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            Button v_Button = sender as Button;
            Tuple<int, int> v_ChosenCell;
            eGameStatus v_CurrentGameStatus;

            v_Button.Enabled = false;
            if(m_Game.PlayerTurn == ePlayerTurn.PlayerO)
            {
                v_Button.Text = k_O;
                v_ChosenCell = returnChosenCell(v_Button);
                playerTurnHelper(v_ChosenCell);

                if (m_Game.Users[m_Game.ReturnCurrentUserIndex()].IsComputer)
                {
                    v_ChosenCell = m_Game.Table.ChooseRandomCellForComputerUser();
                    m_ButtonsArr[v_ChosenCell.Item1, v_ChosenCell.Item2].Text = k_X;
                    m_ButtonsArr[v_ChosenCell.Item1, v_ChosenCell.Item2].Enabled = false;
                    playerTurnHelper(v_ChosenCell);
                }
            }
            else
            {
                v_Button.Text = k_X;
                v_ChosenCell = returnChosenCell(v_Button);
                playerTurnHelper(v_ChosenCell);
            }
        }

        private void playerTurnHelper(Tuple<int, int> i_ChosenCell)
        {
            eGameStatus v_CurrentGameStatus;
            v_CurrentGameStatus = m_Game.GameRound(i_ChosenCell);
            checkGameStatus(v_CurrentGameStatus);
        }

        private void showScoreLabel()
        {
            labelPlayer1.Text = m_Game.Users[0].Name + ": " + m_Game.Users[0].Score;
            labelPlayer2.Text = m_Game.Users[1].Name + ": " + m_Game.Users[1].Score;
            labelPlayer1.Left = Size.Width / 2 - labelPlayer2.Width - k_Spacing;
            labelPlayer2.Left = Size.Width / 2 + k_Spacing;
        }

        private Tuple<int, int> returnChosenCell(Button i_ChosenButton)
        {
            Tuple<int, int> v_ChosenCell;
            string[] v_ButtonPosition = i_ChosenButton.Name.Split('_');

            v_ChosenCell = new Tuple<int, int>(int.Parse(v_ButtonPosition[0]), int.Parse(v_ButtonPosition[1]));

            return v_ChosenCell;
        }

        private void checkGameStatus(eGameStatus i_CurrentGameStatus)
        {
            switch(i_CurrentGameStatus)
            {
                case eGameStatus.Win:
                    winMessageBox();
                    restartGameForm();
                    break;
                case eGameStatus.Tie:
                    tieMessageBox();
                    restartGameForm();
                    break;
                default:
                    break;
            }
        }

        private void tieMessageBox()
        {
            DialogResult v_Message = MessageBox.Show("Tie!\nWould you like to play another round?", "A Tie!", MessageBoxButtons.YesNo);
            if (v_Message == DialogResult.No)
            {
                this.Close();
            }
        }

        private void winMessageBox()
        {
            DialogResult v_Message = MessageBox.Show(string.Format("The winner is {0}!\nWould you like to play another round?", m_Game.Users[m_Game.ReturnCurrentUserIndex()].Name), "A Win!", MessageBoxButtons.YesNo);
            if (v_Message == DialogResult.No)
            {
                this.Close();
            }
        }

        private void restartGameForm()
        {
            foreach(Button v_Button in m_ButtonsArr)
            {
                v_Button.Text = "";
                v_Button.Enabled = true;
            }

            m_Game.Table.RestartInformationInTable();
            m_Game.PlayerTurn = ePlayerTurn.PlayerO;
            showScoreLabel();
        }
    }
}
