using System.Windows;

namespace Numberle
{
    public partial class CustomMessageBox : Window
    {
        public bool IsDaily { get; private set; }

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void Daily_Click(object sender, RoutedEventArgs e)
        {
            IsDaily = true;
            DialogResult = true;
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            IsDaily = false;
            DialogResult = true;
        }
    }
}
