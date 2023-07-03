using System.Windows.Forms;
using Logic;

namespace UI
{
    public class Program
    {
        public static void Main()
        {
            Game v_Start = new Game();

            GameSettingsForm v_GameSettingsForm = new GameSettingsForm(v_Start);
            v_GameSettingsForm.ShowDialog();

            TicTacToeMisereForm v_TicTacToeMisereForm = new TicTacToeMisereForm(v_Start);
            v_TicTacToeMisereForm.ShowDialog();
        }
    }
}
