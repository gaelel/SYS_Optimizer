using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace OptimizationAPP
{
    public partial class TempFilesPage : Page
    {
        public TempFilesPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var resumen = new System.Text.StringBuilder();
            long espacioLiberado = 0;
            int archivosBorrados = 0;
            int archivosFallidos = 0;
            string tempPath = Path.GetTempPath();

            try
            {
                var directory = new DirectoryInfo(tempPath);

                foreach (var file in directory.GetFiles())
                {
                    try 
                    { 
                        espacioLiberado = file.Length;
                        file.Delete(); 
                        archivosBorrados++;
                    }
                    catch 
                    {
                        archivosFallidos++;
                    }
                }

                foreach (var subDir in directory.GetDirectories())
                {
                    try { subDir.Delete(true); }
                    catch { /*Carpeta en uso*/ }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al limpiar archivos temporales: {ex.Message}");
            }

            resumen.AppendLine($"Se eliminaron {archivosBorrados} archivos temporales, liberando {espacioLiberado / (1024.0 * 1024.0):F2} MB.");

            if (archivosFallidos > 0) resumen.AppendLine($"{archivosFallidos} archivos no pudieron ser eliminados porque estaban en uso.");

            MessageBox.Show(resumen.ToString(), "Limpieza Completada");
        }

        private void btnSiguiente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new DiskPage());
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
