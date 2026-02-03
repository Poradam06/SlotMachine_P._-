using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SlotMachine_P
{
    public partial class SpinWindow : Window
    {
        int balance;
        int spinCost;
        int reelCount;
        int failCount = 0;

        public SpinWindow(int currentBalance, int cost, int reels)
        {
            InitializeComponent();

            balance = currentBalance;
            spinCost = cost;
            reelCount = reels;

            BalanceText.Text = "Egyenleg: " + balance;

            Spin();
        }
        
        private async Task Spin()
        {
            BalanceText.Text = "Egyenleg: " + balance;
            Random rnd = new Random();
            string[] symbols = { "🍒", "🍋", "🍉", "⭐", "7", "🔔", "🍀", "$", "🍎", "❤︎⁠", "🍇", "💎", "🍊", "Ω", "🍉" };

            string[] results = new string[reelCount];
            bool nyert = false;
            ReelsPanel.Children.Clear();

            for (int i = 0; i < reelCount; i++)
            {
                results[i] = symbols[rnd.Next(symbols.Length)];
            }
            foreach (var asd in results)
            {
                TextBlock tb = new TextBlock
                {
                    Text = asd,
                    FontSize = 32,
                    Margin = new Thickness(5)
                };
                ReelsPanel.Children.Add(tb);
                
            }
            var numUniques = 1;
            nyert = results.Distinct().Count() == numUniques;

            BalanceText.Text = "Egyenleg: " + balance;

            if (nyert)
            {
                NewSpin.IsEnabled = false;
                ContinueCheck.IsEnabled = true;
                ContinueCheck.IsChecked = false;
                if (results[0] == "7") balance += spinCost*10*failCount;
                else balance += spinCost * 2*failCount;
                failCount = 0;
                ResultText.Text = "🎉 Nyertél!";
                BalanceText.Text = "Egyenleg: " + balance;
                ResultText.Foreground = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
                failCount++;
                ResultText.Text = "😢 Nem nyertél!";
                ResultText.Foreground = System.Windows.Media.Brushes.IndianRed;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(balance);
            main.Show();
            this.Close();
        }

        private void NewSpin_Click(object sender, RoutedEventArgs e)
        {
            ContinueCheck.IsEnabled = false;
            if (balance < spinCost)
            {
                MessageBox.Show("Nincs elég egyenleg!");
                return;
            }

            balance -= spinCost;
            Spin();
        }

        private void ContCheck(object sender, RoutedEventArgs e)
        {
            NewSpin.IsEnabled = true;
        }
    }
}