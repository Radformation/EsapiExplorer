using System.Windows;
using EsapiExplorer;
using VMS.TPS.Common.Model.API;

namespace VMS.TPS
{
    public class Script
    {
        public void Execute(ScriptContext context, Window window)
        {
            var viewer = new ObjectViewer();
            viewer.Load(context, nameof(ScriptContext));

            window.Content = viewer;
            window.Title = "ESAPI Explorer";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 800;
        }
    }
}
