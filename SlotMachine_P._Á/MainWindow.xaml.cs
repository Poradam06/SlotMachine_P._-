using System;
using System.Windows;
using System.Windows.Controls;

namespace SlotMachine_P
{
    public partial class MainWindow : Window
    {
        int balance = 100000;
        public int spinCost = 50;

        public MainWindow()
        {
            InitializeComponent();
            balance = 100000;
            BalanceText.Text = "Egyenleg: " + balance;
            CostText.Text = "Pörgetés ára: " + spinCost;
        }

        public MainWindow(int newBalance) : this()
        {
            balance = newBalance;
            BalanceText.Text = "Egyenleg: " + balance;
        }

        //Porgetes ara
        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            if (CostText == null)
                return;

            if (Radio3.IsChecked == true)
            {
                spinCost = 50;
                CostText.Text = "Pörgetés ára:" + spinCost;
            }


            else if (Radio5.IsChecked == true)
            {
                spinCost = 100;
                CostText.Text = "Pörgetés ára: " + spinCost;
            }



        }



        private void SpinButton_Click(object sender, RoutedEventArgs e)
        {
            if (balance < spinCost)
            {
                MessageBox.Show("Nincs elég egyenleg!");
                return;
            }

            balance -= spinCost;

            int reelCount = Radio3.IsChecked == true ? 3 : 5;

            SpinWindow spinWindow = new SpinWindow(balance, spinCost, reelCount);
            spinWindow.Show();

            this.Close();
        }


    }

}
       
    

