using System.Configuration;
using System.Data;
using System.Windows;

namespace Numberle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.NewGame_Click(null, null);
        }
    }

}
