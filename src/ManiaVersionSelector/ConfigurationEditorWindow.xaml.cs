using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManiaVersionSelector
{
    /// <summary>
    /// Interaction logic for ConfigurationEditorWindow.xaml
    /// </summary>
    public partial class ConfigurationEditorWindow : Window
    {
        public ConfigurationEditorWindow(ConfigurationViewModel viewModel)
        {
            this.DataContext = this.ViewModel = viewModel;
            InitializeComponent();
        }

        protected ConfigurationViewModel ViewModel { get; private set; }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Configuration.ApplyTo(this.ViewModel.OriginalConfiguration);
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Configuration.Versions.Add(new VersionEntry());
        }

        private void RemoveItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.SelectedVersion != null)
            {
                this.ViewModel.Configuration.Versions.Remove(this.ViewModel.SelectedVersion);
            }
        }

        private void PreviewButtonClick(object sender, RoutedEventArgs e)
        {
            new MainWindow(new ManiaVersionSelectorViewModel() { PreviewMode = true, Configuration = this.ViewModel.Configuration }).ShowDialog();
        }
    }
}
