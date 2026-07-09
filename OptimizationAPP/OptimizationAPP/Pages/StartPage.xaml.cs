using System.Windows.Controls;

namespace OptimizationAPP
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void btnSiguiente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new TempFilesPage());
        }
    }
}
