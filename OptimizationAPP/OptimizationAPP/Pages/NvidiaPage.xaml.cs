using OptimizationAPP.Utilitys;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationAPP.Pages
{
    public partial class NvidiaPage : Page
    {
        public NvidiaPage()
        {
            InitializeComponent();

            if (!PermisosHelper.IsAdmin())
            {
                btnSiguiente.Content = "TERMINAR";
            }
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (PermisosHelper.IsAdmin())
            {
                NavigationService.Navigate(new Pages.DeepTempPage());
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
