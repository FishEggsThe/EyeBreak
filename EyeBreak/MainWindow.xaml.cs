using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EyeBreak.View;

namespace EyeBreak
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTimes();
        }

        private void btnStartTimer_Click(object sender, RoutedEventArgs e) {
            object waitTime = tiWait.lvTimes.SelectedItem;
            object breakTime = tiBreak.lvTimes.SelectedItem;

            if (waitTime == null || breakTime == null) {
                MessageBox.Show("Please select a time from both lists");
                return;
            }

            TimerWindow timerWindow = new TimerWindow((string)waitTime, (string)breakTime);
            Opacity = 0.3;
            timerWindow.ShowDialog();
            Opacity = 1;
        }
        private void LoadTimes() {
            foreach (var time in Settings.Default.WaitTimes)
                tiWait.lvTimes.Items.Add(time);
            foreach (var time in Settings.Default.BreakTimes)
                tiBreak.lvTimes.Items.Add(time);
        }
    }
}