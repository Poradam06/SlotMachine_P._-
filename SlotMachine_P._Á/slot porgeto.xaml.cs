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
        
        private void Spin()
        {
            BalanceText.Text = "Egyenleg: " + balance;
            Random rnd = new Random();
            string[] symbols = { "🍒", "🍋", "🔔", "💎", "7️⃣" };

            string[] results = new string[reelCount];
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

            BalanceText.Text = "Egyenleg: " + balance;

            var numUniques = 1;
            bool fullMatch = results.Distinct().Count() == 1;
            bool haromEgymasMellett = false;
            if (reelCount >= 3)
            {
                for (int i = 0; i <= results.Length - 3; i++)
                {
                    if (results[i] == results[i + 1] && results[i] == results[i + 2])
                    {
                        haromEgymasMellett = true;
                        break;
                    }
                }
            }

            if (fullMatch)
            {
                NewSpin.IsEnabled = false;
                ContinueCheck.IsEnabled = true;
                ContinueCheck.IsChecked = false;
                if (results[0] == "7️⃣") balance += spinCost*10*failCount;
                else balance += spinCost * 2*failCount;
                failCount = 0;
                ResultText.Text = "🎉 Nyertél!";
                BalanceText.Text = "Egyenleg: " + balance;
                ResultText.Foreground = System.Windows.Media.Brushes.LightGreen;
            }
            else if (haromEgymasMellett)
            {
                balance += spinCost*2*(1+failCount/10); // kisebb nyeremény
                ResultText.Text = "🙂 3 egyforma! Kis nyeremény!";
                ResultText.Foreground = System.Windows.Media.Brushes.Yellow;
                NewSpin.IsEnabled = false;
                ContinueCheck.IsEnabled = true;
                ContinueCheck.IsChecked = false;
                failCount = 0;
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