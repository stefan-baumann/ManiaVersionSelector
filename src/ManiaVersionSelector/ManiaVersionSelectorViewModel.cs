using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Media;

namespace ManiaVersionSelector
{
    public class ManiaVersionSelectorViewModel
    {
        public string FilePath { get; set; }

        public string FileName => Path.GetFileName(this.FilePath);

        public Configuration Configuration { get; } = Configuration.Instance ?? new Configuration();

        public VersionEntry SelectedVersion { get; set; }
    }
}
