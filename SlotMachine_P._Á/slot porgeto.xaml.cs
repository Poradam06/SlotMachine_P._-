using System;
using System.Windows;
using System.Windows.Controls;

namespace SlotMachine_P
{
    public partial class SpinWindow : Window
    {
        int balance;
        int spinCost;
        int reelCount;

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
            string[] symbols = { "🍒", "🍋", "🍉", "⭐", "7", "🔔", "🍀", "$", "🍎", "❤︎⁠", "🍇", "💎", "🍊", "Ω", "🍉" };
            Random rnd = new Random();

            string[] results = new string[reelCount];
            bool nyert = false;
            ReelsPanel.Children.Clear();

            for (int i = 0; i < reelCount; i++)
            {
                results[i] = symbols[rnd.Next(symbols.Length)];
            }

            foreach(var asd in results)
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
                if (results[0] == "7") balance += spinCost*10;
                else balance += spinCost * 2;
                ResultText.Text = "🎉 Nyertél!";
                BalanceText.Text = "Egyenleg: " + balance;
                ResultText.Foreground = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
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
            if (balance < spinCost)
            {
                MessageBox.Show("Nincs elég egyenleg!");
                return;
            }

            balance -= spinCost;
            Spin();
        }
    }
}