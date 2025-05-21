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
        }

        private void btnStartTimer_Click(object sender, RoutedEventArgs e) {
            object waitTime = tiWait.lvTimes.SelectedItem;
            object breakTime = tiBreak.lvTimes.SelectedItem;

            TimerWindow timerWindow = new TimerWindow((string)waitTime, (string)breakTime);
            //TimerWindow timerWindow = new TimerWindow("fiddy", "also fiddy");
            Opacity = 0.3;
            timerWindow.ShowDialog();
            Opacity = 1;
        }
    }
}