using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private Game m_Game;

        public TicTacToeMisereForm(Game i_Game)
        {
            m_Game = i_Game;

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
                    v_Button.Name = string.Format("button{0}_{1}", v_Row, v_Col);
                    v_Button.Click += button_Click;
                    Controls.Add(v_Button);
                }
            }
        }
        
        private void button_Click(object sender, EventArgs e)
        {

        }

        private void showScoreLabel()
        {
            labelPlayer1.Text = m_Game.Users[0].Name + ": " + m_Game.Users[0].Score;
            labelPlayer2.Text = m_Game.Users[1].Name + ": " + m_Game.Users[1].Score;
            labelPlayer1.Left = Size.Width / 2 - labelPlayer2.Width - k_Spacing;
            labelPlayer2.Left = Size.Width / 2 + k_Spacing;
        }
    }
}
