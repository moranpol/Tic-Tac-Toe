using System;
using System.Linq;
using System.Windows.Forms;

namespace XMixDrixUpsideDown
{
    public partial class GameSettingsForm : Form
    {
        private Game m_Game;

        public GameSettingsForm(Game i_Game)
        {
            m_Game = i_Game;
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text.Length == 0) 
            {
                textBoxPlayer1.Text = "Player 1";
            }

            if(textBoxPlayer2.Text.Length == 0)
            {
                textBoxPlayer1.Text = "Player 2";
            }
            else if (!checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Text = "Computer";
            }

            m_Game.CreateUser(0, false, textBoxPlayer1.Text);
            m_Game.CreateUser(1, !checkBoxPlayer2.Checked, textBoxPlayer2.Text);

            m_Game.Table.SizeOfTable = (int)numericUpDownRow.Value;
            m_Game.Table.RestartInformationInTable();
            Close();
        }

        private void numericUpDownRow_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCol.Value = numericUpDownRow.Value;
        }

        private void numericUpDownCol_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRow.Value = numericUpDownCol.Value;
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Text = "";
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.Enabled = false;
            }
        }
    }
}
