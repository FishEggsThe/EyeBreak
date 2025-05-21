using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EyeBreak.View
{
    public partial class TimerWindow : Window
    {
        int waitTimer;
        int breakTimer;
        System.Windows.Threading.DispatcherTimer timer;
        public TimerWindow(string waitTime, string breakTime)
        {
            InitializeComponent();

            txtTimer.Text = $"{waitTime}m and {breakTime}s";
            waitTimer = Int32.Parse(waitTime.TrimStart('*'));
            breakTimer = Int32.Parse(breakTime.TrimStart('*'));
            
        }

        private void StartTimer(object sender, EventArgs e) {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(UpdateWaitTimer);
            timer.Interval = new TimeSpan(0, 5, 0);
            timer.Start();
        }

        private void UpdateWaitTimer(object sender, EventArgs e) {
            waitTimer--;
            txtTimer.Text = $"{waitTimer}";

            if (waitTimer <= 0) { timer.Stop(); }
            CommandManager.InvalidateRequerySuggested();
        }
        private void UpdateBreakTimer(object sender, EventArgs e) {
            breakTimer--;
            txtTimer.Text = $"{breakTimer}";

            if (breakTimer <= 0) { timer.Stop(); }
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
