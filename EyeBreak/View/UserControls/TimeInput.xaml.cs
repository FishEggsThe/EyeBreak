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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EyeBreak.View.UserControls
{
    public partial class TimeInput : UserControl {
        int numOfDefaultTimes;

        public TimeInput()
        {
            InitializeComponent();
            lvTimes.Items.Add("20");
            lvTimes.Items.Add("30");
            lvTimes.Items.Add("40");
            numOfDefaultTimes = lvTimes.Items.Count;
        }

        private string[] customTimes = Array.Empty<string>();
        public string[] CustomTimes {
            get { return customTimes; }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            string time = txtInput.Text;
            if (int.TryParse(time, out int value)) {
                foreach(var item in lvTimes.Items) {
                    var itemString = item.ToString();
                    if (itemString == time || itemString == $"*{time}") {
                        MessageBox.Show("Duplicate entry, please try again");
                        return;
                    }
                }
                lvTimes.Items.Add("*" + time);
                //SaveTime(time);
            } else {
                MessageBox.Show("Please input a whole number");
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e) {
            int index = lvTimes.SelectedIndex;

            if (index >= numOfDefaultTimes) {
                string time = (string)lvTimes.SelectedItem;
                lvTimes.Items.RemoveAt(index);
                //DeleteTime(time);
            } else {
                MessageBox.Show("Only custom numbers can be deleted");
            }
        }

        private void SaveTime(string time) {
            if (Name == "tiWait")
                Settings.Default.WaitTimes.Add(time);
            else if (Name == "tiBreak")
                Settings.Default.BreakTimes.Add(time);
        }
        private void DeleteTime(string time) {
            if (Name == "tiWait")
                Settings.Default.WaitTimes.Add(time);
            else if (Name == "tiBreak")
                Settings.Default.BreakTimes.Add(time);
        }
    }
}
