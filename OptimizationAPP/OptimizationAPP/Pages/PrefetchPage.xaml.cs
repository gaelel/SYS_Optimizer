using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationAPP.Pages
{
    public partial class PrefetchPage : Page
    {
        public PrefetchPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            var resumen = new System.Text.StringBuilder();
            long espacioLiberado = 0;
            int archivosBorrados = 0;
            int archivosEnUso = 0;

            string prefetchPath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "Prefetch");

            if (!Directory.Exists(prefetchPath))
            {
                MessageBox.Show("La carpeta Prefetch no existe en este sistema.", "Aviso");
                return;
            }
                
            var directory = new DirectoryInfo(prefetchPath);

            foreach (var file in directory.GetFiles("*.pf"))
            {
                try
                {
                    espacioLiberado += file.Length;
                    file.Delete();
                    archivosBorrados++;
                }
                catch
                {
                    archivosEnUso++;
                }
            }

            resumen.AppendLine($"Se eliminaron {archivosBorrados} archivos de Prefetch, liberando {espacioLiberado / (1024.0 * 1024.0):F2} MB.");

            if (archivosEnUso > 0) resumen.AppendLine($"{archivosEnUso} archivos no pudieron ser eliminados porque estaban en uso.");
            
            MessageBox.Show(resumen.ToString(), "Limpieza Completada");
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EventVwrPage());
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
