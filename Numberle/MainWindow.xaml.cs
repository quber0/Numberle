using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
                if (!char.IsDigit(c) || char.IsWhiteSpace(c)) // Sprawdź, czy znak nie jest cyfrą lub jest spacją
                {
                    e.Handled = true; // Zablokuj wprowadzenie znaku
                    return;
                }
            }
        }
        private void CheckGuess_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAttempt < 6)
            {
                string tempSecretNumber = SecretNumber.ToString("D5"); // Upewnij się, że SecretNumber ma 5 cyfr

                // Sprawdź, czy wszystkie TextBox w bieżącym wierszu są wypełnione
                for (int column = 0; column < 5; column++)
                {
                    string textBoxName = $"Guess_{CurrentAttempt - 1}_{column}";
                    TextBox? textBox = (TextBox?)FindName(textBoxName);

                    if (textBox != null && string.IsNullOrEmpty(textBox.Text))
                    {
                        MessageBox.Show("Wszystkie pola muszą być wypełnione przed zatwierdzeniem!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Zlicz wystąpienia cyfr w SecretNumber
                var secretDigitCounts = new Dictionary<char, int>();
                foreach (char digit in tempSecretNumber)
                {
                    if (secretDigitCounts.ContainsKey(digit))
                        secretDigitCounts[digit]++;
                    else
                        secretDigitCounts[digit] = 1;
                }

                int guessedDigitAmount = 0;

                // Pierwsza pętla: kolorowanie cyfr poprawnych i na właściwej pozycji
                for (int column = 0; column < 5; column++)
                {
                    string textBoxName = $"Guess_{CurrentAttempt - 1}_{column}";
                    TextBox? textBox = (TextBox?)FindName(textBoxName);

                    if (textBox != null)
                    {
                        char guessedDigit = textBox.Text[0];
                        char secretDigit = tempSecretNumber[column];

                        if (guessedDigit == secretDigit)
                        {
                            guessedDigitAmount++;
                            textBox.Background = Brushes.Green; // Cyfra poprawna i na właściwej pozycji
                            secretDigitCounts[guessedDigit]--; // Zmniejsz licznik dla tej cyfry
                        }
                    }
                }

                // Druga pętla: kolorowanie cyfr poprawnych, ale na niewłaściwej pozycji
                for (int column = 0; column < 5; column++)
                {
                    string textBoxName = $"Guess_{CurrentAttempt - 1}_{column}";
                    TextBox? textBox = (TextBox?)FindName(textBoxName);

                    if (textBox != null && textBox.Background != Brushes.Green) // Pomijaj już oznaczone na zielono
                    {
                        char guessedDigit = textBox.Text[0];

                        if (secretDigitCounts.ContainsKey(guessedDigit) && secretDigitCounts[guessedDigit] > 0)
                        {
                            textBox.Background = Brushes.Yellow; // Cyfra poprawna, ale na niewłaściwej pozycji
                            secretDigitCounts[guessedDigit]--; // Zmniejsz licznik dla tej cyfry
                        }
                        else
                        {
                            textBox.Background = Brushes.Gray; // Nadmiarowa cyfra
                        }
                    }
                }

                // Sprawdź, czy wszystkie cyfry są poprawne
                if (guessedDigitAmount == 5)
                {
                    MessageBox.Show("Wygrałeś! Liczba to " + SecretNumber.ToString());
                    DisableTextBoxesInRow(CurrentAttempt);
                    foreach (var child in MainGrid.Children)
                    {
                        if (child is TextBox textBox && Grid.GetRow(textBox) == CurrentAttempt)
                        {
                            textBox.IsEnabled = false;
                        }
                    }
                    return;
                }
                else
                {
                    DisableTextBoxesInRow(CurrentAttempt);
                    CurrentAttempt++;
                }
            }
            else
            {
                MessageBox.Show("Przegrałeś! Liczba to " + SecretNumber.ToString());
                DisableTextBoxesInRow(CurrentAttempt);
                foreach (var child in MainGrid.Children)
                {
                    if (child is TextBox textBox && Grid.GetRow(textBox) == CurrentAttempt)
                    {
                        textBox.IsEnabled = false;
                    }
                }
            }
        }




        public void NewGame_Click(object sender, RoutedEventArgs e)
        {
            CurrentAttempt = 1;
            var customMessageBox = new CustomMessageBox();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string textBoxName = $"Guess_{i}_{j}";
                    TextBox? textBox = (TextBox?)FindName(textBoxName);
                    if (textBox != null)
                    {
                        textBox.Text = "";
                        textBox.Background = Brushes.White;
                        textBox.IsEnabled = (i == 0);
                    }
                }
            }
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
