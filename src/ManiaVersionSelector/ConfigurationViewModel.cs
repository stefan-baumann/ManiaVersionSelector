using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ManiaVersionSelector
{
    public class ConfigurationViewModel
    {
        public ConfigurationViewModel(Configuration configuration)
        {
            this.OriginalConfiguration = configuration;
            this.Configuration = configuration.CreateCopy();
        }

        public Configuration Configuration { get; set; }

        public Configuration OriginalConfiguration { get; set; }

        public VersionEntry SelectedVersion { get; set; }
    }
}
