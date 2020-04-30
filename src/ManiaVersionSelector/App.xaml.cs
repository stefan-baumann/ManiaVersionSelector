using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            string configPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "configuration.json");
            if (File.Exists(configPath))
            {
                try
                {
                    Configuration.Instance = Configuration.LoadFromFile(configPath);
                }
                catch (JsonReaderException aex)
                {
                    MessageBox.Show("Could not read configuration.json file.");
                }
            }
            else
            {
                Configuration.Instance = new Configuration()
                {
                    Versions = new ObservableCollection<VersionEntry>()
                    {
                        new VersionEntry() { Name = "ManiaPlanet", Path="C:\\...\\ManiaPlanet.exe" },
                        new VersionEntry() { Name = "TrackMania United Forever", Path="C:\\...\\TrackMania United Forever.exe" },
                    }
                };
                new ConfigurationEditorWindow(new ConfigurationViewModel(Configuration.Instance)).ShowDialog();
                Configuration.Instance.SaveToFile(configPath);
            }

            string[] args = e.Args;
            if (args.Length == 1)
            {
                string path = args[0];
                if (File.Exists(path))
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

            new ConfigurationEditorWindow(new ConfigurationViewModel(Configuration.Instance)).ShowDialog();
            Configuration.Instance.SaveToFile(configPath);
            this.Shutdown();
        }

        private void ApplicationDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            this.Shutdown();
        }
    }
}
