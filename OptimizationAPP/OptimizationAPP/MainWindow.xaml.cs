using System.Windows;
using System.Windows.Controls;

namespace OptimizationAPP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            menuFrame.Navigate(new StartPage());
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
    }
}