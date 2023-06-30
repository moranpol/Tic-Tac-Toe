using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMixDrixUpsideDown
{
    public partial class TicTacToeMisereForm : Form
    {
        private const int k_ButtonSize = 50;
        private const int k_Spacing = 5;
        private Game m_Game;

        public TicTacToeMisereForm(Game i_Game)
        {
            m_Game = i_Game;
            InitializeComponent();
            createTable();
        }

        private void createTable()
        {
            int v_SizeOfTable = m_Game.Table.SizeOfTable;

            int formWidth = v_SizeOfTable * (k_ButtonSize + k_Spacing) + k_Spacing;
            int formHeight = v_SizeOfTable * (k_ButtonSize + k_Spacing) + k_Spacing + label1.Height;
            Size = new Size(formWidth, formHeight);

            for (int v_Row = 0; v_Row < v_SizeOfTable; v_Row++)
            {
                for (int v_Col = 0; v_Col < v_SizeOfTable; v_Col++)
                {
                    Button button = new Button();
                    button.Size = new Size(k_ButtonSize, k_ButtonSize);
                    button.Location = new Point(
                        k_Spacing + v_Col * (k_ButtonSize + k_Spacing),
                        k_Spacing + v_Row * (k_ButtonSize + k_Spacing));
                    button.Name = string.Format("button{0}_{1}", v_Row, v_Col);
                    button.Click += button_Click;
                    Controls.Add(button);
                }
            }
        }
        
        private void button_Click(object sender, EventArgs e)
        {

        }
    }
}
