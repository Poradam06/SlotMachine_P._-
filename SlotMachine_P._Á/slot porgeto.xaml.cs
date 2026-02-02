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
            string[] symbols = { "🍒", "🍋", "🍉", "⭐", "7" };
            Random rnd = new Random();

            ReelsPanel.Children.Clear();

            bool nyert = false;

            for (int i = 0; i < reelCount; i++)
            {
                TextBlock tb = new TextBlock
                {
                    Text = symbols[rnd.Next(symbols.Length)],
                    FontSize = 32,
                    Margin = new Thickness(5)
                };

                ReelsPanel.Children.Add(tb);

                if (i > 0 &&
                    tb.Text == ((TextBlock)ReelsPanel.Children[i - 1]).Text)
                {
                    balance += spinCost * 2;
                    nyert = true;
                }
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

    }
}