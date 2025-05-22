using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Numerics;
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
        int waitTimer, breakTimer, state = 0;
        DispatcherTimer timer;
        TimeSpan time;
        SoundPlayer timeForBreakSound, duringBreakSound;

        public TimerWindow(string waitTime, string breakTime)
        {
            DataContext = this;
            InitializeComponent();

            waitTimer = Int32.Parse(waitTime.TrimStart('*'));// * 60;
            breakTimer = Int32.Parse(breakTime.TrimStart('*'));

            timeForBreakSound = new SoundPlayer("C:\\VisualStudio2022Projects\\EyeBreak\\EyeBreak\\Sounds\\car_crashing_t2.wav");
            duringBreakSound = new SoundPlayer("C:\\VisualStudio2022Projects\\EyeBreak\\EyeBreak\\Sounds\\Hud_specialtric.wav");

            UpdateState();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string buttonText;
        public string ButtonText {
            get { return buttonText; }
            set { buttonText = value; OnPropertyChanged(); }
        }

        private void SetState(int stateNum) {
            state = stateNum;
            UpdateState();
        }
        private void IncrementState() {
            state++;
            UpdateState();
        }
        private void PlaySound(SoundPlayer player) {
            player.Load();
            player.PlayLooping();
        }
        private void StopSound(SoundPlayer player) {
            player.Stop();
        }
        private void UpdateState() {
            switch (state) {
                case 0:
                    StartTimer(waitTimer);
                    ButtonText = "Wait";
                    break;
                case 1:
                    btnChangeState.IsEnabled = true;
                    PlaySound(timeForBreakSound);
                    ButtonText = "Start Break";
                    break;
                case 2:
                    StartTimer(breakTimer);
                    StopSound(timeForBreakSound);
                    PlaySound(duringBreakSound);
                    ButtonText = "Patience";
                    break;
                case 3:
                    btnChangeState.IsEnabled = true;
                    StopSound(duringBreakSound);
                    ButtonText = "Start Timers Again";
                    break;
                default:
                    SetState(0);
                    break;
            }
        }

        private void btnChangeState_Click(object sender, RoutedEventArgs e) {
            IncrementState();
        }

        private string currentTime;
        public string CurrentTime {
            get { return currentTime; }
            set { currentTime = value; OnPropertyChanged(); }
        }

        private void StartTimer(int seconds) {
            btnChangeState.IsEnabled = false;
            time = TimeSpan.FromSeconds(seconds);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer);
            timer.Interval = new TimeSpan(0, 0, 1);
            CurrentTime = time.ToString("c");
            timer.Start();
        }

        private void UpdateTimer(object sender, EventArgs e) {
            if (time == TimeSpan.Zero) {
                timer.Stop();
                IncrementState();
            } else {
                time = time.Add(TimeSpan.FromSeconds(-1));
                CurrentTime = time.ToString("c");
            }
            CommandManager.InvalidateRequerySuggested();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            timer.Stop();
        }
    }
}
