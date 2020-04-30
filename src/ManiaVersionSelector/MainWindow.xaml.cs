using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            : this(new ManiaVersionSelectorViewModel())
        { }

        public MainWindow(ManiaVersionSelectorViewModel viewModel)
        {
            InitializeComponent();

            this.ViewModel = viewModel;
            this.DataContext = viewModel;
            FocusManager.SetFocusedElement(this, this.VersionList);
        }

        public ManiaVersionSelectorViewModel ViewModel { get; private set; }

        private void VersionListKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Space))
            {
                if (!this.ViewModel.PreviewMode)
                {
                    this.Launch();
                }
                else
                {
                    this.Close();
                }
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void VersionListMouseClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.ViewModel.PreviewMode)
            {
                this.Launch();
            }
            else
            {
                this.Close();
            }
        }

        private void Launch()
        {
            this.Close();
            ManiaVersionSelectorViewModel viewModel = ((ManiaVersionSelectorViewModel)this.DataContext);
            VersionEntry selectedVersion = viewModel.SelectedVersion;
            string path = viewModel.FilePath;
            if (File.Exists(selectedVersion.Path))
            {
                Process.Start(selectedVersion.Path, $"\"{path}\"");
            }
            else
            {
                MessageBox.Show("Could not find executable at " + selectedVersion.Path);
            }
        }
    }
}
