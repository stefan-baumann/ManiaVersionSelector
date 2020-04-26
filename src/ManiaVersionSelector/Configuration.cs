using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Media;

namespace ManiaVersionSelector
{
    public class Configuration
    {
        public List<VersionEntry> Versions { get; set; }

        public static Configuration Instance { get; set; }

        public static Configuration LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(path));
        }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }

    public class VersionEntry
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
        public string Name { get; set; }

        public string Path { get; set; }
    }
}
