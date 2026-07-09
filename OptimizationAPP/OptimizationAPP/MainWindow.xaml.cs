using System.Windows;
using System.Windows.Controls;
using OptimizationAPP;
using OptimizationAPP.Pages;
using OptimizationAPP.Utilitys;

namespace OptimizationAPP
{
    public partial class MainWindow : Window
    {
        private bool _isAdmin;
        public MainWindow()
        {
            InitializeComponent();
            _isAdmin = PermisosHelper.IsAdmin();
            ConfigurarAccesoModulos();
            menuFrame.Navigate(new StartPage());
            ActualizarBotonesIdioma(Properties.Settings.Default.Language);
        }

        private void ConfigurarAccesoModulos()
        {
            btnDeepTempFiles.IsEnabled = _isAdmin;
            btnPrefetch.IsEnabled = _isAdmin;
            btnEventViewer.IsEnabled = _isAdmin;
            btnAdvancedDisk.IsEnabled = _isAdmin;

            if (!_isAdmin)
            {
                string message = "Algunos módulos requieren privilegios de administrador. Por favor, reinicie la aplicación como administrador para acceder a todas las funciones.";
                btnDeepTempFiles.ToolTip = message;
                btnPrefetch.ToolTip = message;
                btnEventViewer.ToolTip = message;
                btnAdvancedDisk.ToolTip = message;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new StartPage());
        }

        private void btnTemp_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new TempFilesPage());
        }

        private void btnDisk_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new DiskPage());
        }

        private void btnDeepTemp_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new Pages.DeepTempPage());
        }

        private void btnPrefetch_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new Pages.PrefetchPage());
        }

        private void btnEvent_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new Pages.EventVwrPage());
        }

        private void btnAdvDisk_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new Pages.AdvDiskPage());
        }

        private void btnNvidia_Click(object sender, RoutedEventArgs e)
        {
            menuFrame.Navigate(new NvidiaPage());
        }

        private void ActualizarBotonesIdioma(string idiomaActivo)
        {
            if (idiomaActivo == "es")
            {
                btnEsp.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#008080"));
                btnEsp.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00FFFF"));
                btnEsp.BorderBrush = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00FFFF"));

                btnEng.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                btnEng.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#9E9E9E"));
                btnEng.BorderBrush = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#333333"));
            }
            else
            {
                btnEng.Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#008080"));
                btnEng.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00FFFF"));
                btnEng.BorderBrush = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00FFFF"));

                btnEsp.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                btnEsp.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#9E9E9E"));
                btnEsp.BorderBrush = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#333333"));
            }
        }

        private void btnEng_Click(object sender, RoutedEventArgs e)
        {
            LanguageManager.SetLanguage("en");
            ActualizarBotonesIdioma("en");
        }

        private void btnEsp_Click(object sender, RoutedEventArgs e)
        {
            LanguageManager.SetLanguage("es");
            ActualizarBotonesIdioma("es");
        }
    }
}