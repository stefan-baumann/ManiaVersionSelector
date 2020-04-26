using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ManiaVersionSelector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            try
            {
                string configPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "configuration.json");
                if (File.Exists(configPath))
                {
                    try
                    {
                        Configuration.Instance = Configuration.LoadFromFile(configPath);
                    }
                    catch (JsonReaderException ex)
                    {
                        MessageBox.Show("Could not read configuration.json file.");
                    }
                }
                else
                {
                    new Configuration()
                    {
                        Versions = new List<VersionEntry>()
                    {
                        new VersionEntry() { Name = "ManiaPlanet", Path="C:\\...\\ManiaPlanet.exe" },
                        new VersionEntry() { Name = "TrackMania United Forever", Path="C:\\...\\TrackMania United Forever.exe" },
                    }
                    }.SaveToFile(configPath);
                    MessageBox.Show("No configuation.json file found in the application directory. Created template file.");
                    //new ConfigurationEditorWindow().ShowDialog();
                }

                string[] args = e.Args;
#if DEBUG
            args = new[] { "C:\\TestFile.Map.Gbx" };
#endif
                if (args.Length == 1)
                {
                    string path = args[0];
#if DEBUG
                if (true)
#else
                    if (File.Exists(path))
#endif
                    {
                        if (path.ToLowerInvariant().EndsWith(".gbx"))
                        {
                            new MainWindow(new ManiaVersionSelectorViewModel() { FilePath = path }).Show();
                            return;
                        }
                    }

                    MessageBox.Show("Unknown file type: " + path);
                    this.Shutdown();
                    return;
                }

                MessageBox.Show("Unknown action.");
                this.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown();
            }
        }

        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            this.Shutdown();
        }
    }
}
