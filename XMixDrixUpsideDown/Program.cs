using System.Windows.Forms;

namespace XMixDrixUpsideDown
{
    public class Program
    {
        public static void Main()
        {
            Game v_Start = new Game();

            GameSettingsForm v_GameSettingsForm = new GameSettingsForm(v_Start);
            Application.Run(v_GameSettingsForm);
            //v_GameSettingsForm.Show();

            TicTacToeMisereForm v_TicTacToeMisereForm = new TicTacToeMisereForm(v_Start);
            Application.Run(v_TicTacToeMisereForm);
            v_TicTacToeMisereForm.Show();
        }
    }
}
