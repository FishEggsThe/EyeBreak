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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EyeBreak.View
{
    public partial class TimerWindow : Window
    {
        int waitTimer, breakTimer;
        DispatcherTimer timer;
        TimeSpan time;
        public TimerWindow(string waitTime, string breakTime)
        {
            DataContext = this;
            InitializeComponent();

            waitTimer = Int32.Parse(waitTime.TrimStart('*'));
            breakTimer = Int32.Parse(breakTime.TrimStart('*'));

            StartTimer(waitTimer);
        }

        private string currentTime;
        public string CurrentTime {
            get { return currentTime; }
            set { currentTime = value; }
        }

        private void StartTimer(int minutes) {
            time = TimeSpan.FromMinutes(waitTimer);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void UpdateTimer(object sender, EventArgs e) {
            if (time == TimeSpan.Zero) {
                timer.Stop();
            } else {
                time = time.Add(TimeSpan.FromSeconds(-1));
                CurrentTime = time.ToString("c");
            }
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
