using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TimerWindow : Window, INotifyPropertyChanged {
        int waitTimer, breakTimer;
        DispatcherTimer timer;
        TimeSpan time;
        public TimerWindow(string waitTime, string breakTime)
        {
            DataContext = this;
            InitializeComponent();

            waitTimer = Int32.Parse(waitTime.TrimStart('*')) * 60;
            breakTimer = Int32.Parse(breakTime.TrimStart('*'));

            StartTimer(waitTimer);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string currentTime;
        public string CurrentTime {
            get { return currentTime; }
            set { currentTime = value; OnPropertyChanged(); }
        }

        private void StartTimer(int seconds) {
            time = TimeSpan.FromSeconds(seconds);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer);
            timer.Interval = new TimeSpan(0, 0, 1);
            CurrentTime = time.ToString("c");
            timer.Start();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateTimer(object sender, EventArgs e) {
            if (time == TimeSpan.Zero) {
                timer.Stop();
            } else {
                time = time.Add(TimeSpan.FromSeconds(-1));
                CurrentTime = time.ToString("c");
                MessageBox.Show(CurrentTime);
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            timer.Stop();
        }
    }
}
