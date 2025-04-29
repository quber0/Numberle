using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Numberle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int SecretNumber { get; set; } = 0;
        public int CurrentAttempt { get; set; } = 1;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CheckIfNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) || c < '0' || c > '9')
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        private void CheckGuess_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAttempt < 8)
            {
                DisableTextBoxesInRow(CurrentAttempt);
                CurrentAttempt++;
            }
        }

        public void NewGame_Click(object sender, RoutedEventArgs e)
        {
            var customMessageBox = new CustomMessageBox();
            if (customMessageBox.ShowDialog() == true)
            {
                if (customMessageBox.IsDaily)
                {
                    int seed = (int)(DateTime.UtcNow.Date - new DateTime(1970, 1, 1)).TotalDays;
                    var rndDaily = new Random(seed);
                    SecretNumber = rndDaily.Next(10000, 100000);
                }
                else
                {
                    var rnd = new Random();
                    SecretNumber = rnd.Next(10000, 100000);
                }
            }
        }


        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void DisableTextBoxesInRow(int currentRowIndex)
        {
            for (int column = 0; column < 5; column++)
            {
                string textBoxName = $"Guess_{CurrentAttempt}_{column}";
                string tempSecretNumber = SecretNumber.ToString();
                if (MainGrid.FindName(textBoxName) is TextBox textBox)
                {
                    if (textBox.Text == tempSecretNumber[column].ToString())
                    {
                        textBox.Background = Brushes.Green;
                    }
                    else if (tempSecretNumber.Contains(textBox.Text))
                    {
                        textBox.Background = Brushes.Yellow;
                    }
                }
            }
            if (currentRowIndex > 0)
            {
                foreach (var child in MainGrid.Children)
                {
                    if (child is TextBox textBox && Grid.GetRow(textBox) == currentRowIndex - 1)
                    {
                        textBox.IsEnabled = false;
                    }
                }
            }

            foreach (var child in MainGrid.Children)
            {
                if (child is TextBox textBox && Grid.GetRow(textBox) == currentRowIndex)
                {
                    textBox.IsEnabled = true;
                }
            }
        }
    }
}