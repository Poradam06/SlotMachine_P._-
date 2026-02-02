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
            string[] symbols = { "🍒", "🍋", "🍉", "⭐", "7" };
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

            if (reelCount==3 && results[0] == results[1] && results[0] == results[2] && results[0] == results[3] && results[0] == results[4])
            {
                balance += spinCost * 2;
                nyert = true;
            } else if (results[0] == results[1] && results[0] == results[2])
            {
                balance += spinCost * 2;
                nyert = true;
            }

                BalanceText.Text = "Egyenleg: " + balance;

            if (nyert)
            {
                ResultText.Text = "🎉 Nyertél!";
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