using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManiaVersionSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ManiaVersionSelectorViewModel();
            FocusManager.SetFocusedElement(this, this.VersionList);
        }

        public MainWindow(ManiaVersionSelectorViewModel model)
        {
            InitializeComponent();

            this.DataContext = model;
            FocusManager.SetFocusedElement(this, this.VersionList);
        }

        private void VersionListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.Launch();
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void VersionListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Launch();
        }

        private void Launch()
        {
            this.Close();
            ManiaVersionSelectorViewModel viewModel = ((ManiaVersionSelectorViewModel)this.DataContext);
            VersionEntry selectedVersion = viewModel.SelectedVersion;
            string path = viewModel.FilePath;
#if DEBUG
            MessageBox.Show($"Starting {selectedVersion.Name}...");
#else
            Process.Start(selectedVersion.Path, path);
#endif
        }
    }
}
