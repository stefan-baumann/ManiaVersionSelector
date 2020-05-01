using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ManiaVersionSelector
{
    public class Configuration
        : INotifyPropertyChanged
    {
        public Configuration()
        {
            this.Versions = new ObservableCollection<VersionEntry>();
            this.Versions.CollectionChanged += (s, e) => this.OnPropertyChanged(nameof(Versions));
        }

        public ObservableCollection<VersionEntry> Versions { get; set; }

        public static Configuration Instance { get; set; }

        public static Configuration LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(path));
        }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public Configuration CreateCopy()
        {
            return new Configuration()
            {
                Versions = new ObservableCollection<VersionEntry>(this.Versions.Select(e => new VersionEntry() { Name = e.Name, Path = e.Path }))
            };
        }

        public void ApplyTo(Configuration other)
        {
            other.Versions = new ObservableCollection<VersionEntry>(this.Versions);
        }
    }

    public class VersionEntry
        : INotifyPropertyChanged
    {
        private ImageSource icon;
        [JsonIgnore]
        public ImageSource Icon
        {
            get
            {
                return icon ?? (icon = IconExtractor.GetIcon(this.Path, false, false));
            }
        }
        private string name;
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        private string path;
        public string Path
        {
            get => this.path;
            set
            {
                this.path = value;
                this.OnPropertyChanged();
                this.icon = null;
                this.OnPropertyChanged(nameof(Icon));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        [JsonIgnore]
        public ICommand BrowsePathCommand => new BrowsePathCommand(this);
    }

    public class BrowsePathCommand
        : ICommand
    {
        public BrowsePathCommand(VersionEntry entry)
        {
            this.Entry = entry;
        }

        public VersionEntry Entry { get; private set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var dialog = new OpenFileDialog() { Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*" };
            if (dialog.ShowDialog() ?? false)
            {
                this.Entry.Path = dialog.FileName;
            }
        }
    }
}
