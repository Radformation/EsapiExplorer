using System.Windows;

namespace EsapiExplorer.Debug
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var viewer = new ObjectViewer();
            var window = new Window();
            window.Content = viewer;
            viewer.Load(this, nameof(App));
            window.Show();
        }
    }
}
