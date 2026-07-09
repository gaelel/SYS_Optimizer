using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationAPP
{
    public partial class DiskPage : Page
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath,uint dwFlags);

        private const uint SHERB_NOCONFIRMATION = 0x00000001;
        private const uint SHERB_NOPROGRESSUI = 0x00000002;
        private const uint SHERB_NOSOUND = 0x00000004;

        public DiskPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            var resumen = new System.Text.StringBuilder();
            long espacioLiberado = 0;

            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
                resumen.AppendLine("Papelera de reciclaje vaciada.");
            }
            catch (Exception ex)
            { 
                /*Papelera de reciclaje vacía o no accesible*/
                resumen.AppendLine($"Error al vaciar la papelera de reciclaje: {ex.Message}");
            }

            int archivosThumbs = 0;
            long espacioThumbs = LimpiarCacheMiniaturas(ref archivosThumbs);
            espacioLiberado += espacioThumbs;
            resumen.AppendLine($"Se eliminaron {archivosThumbs} archivos de caché de miniaturas, liberando {espacioThumbs / (1024.0 * 1024.0):F2} MB.");

            int archivosWER = 0;
            long espacioWER = LimpiarReportesError(ref archivosWER);
            espacioLiberado += espacioWER;
            resumen.AppendLine($"Se eliminaron {archivosWER} archivos de reportes de error, liberando {espacioWER / (1024.0 * 1024.0):F2} MB.");

            resumen.AppendLine();
            resumen.AppendLine($"Total de espacio liberado: {espacioLiberado / (1024.0 * 1024.0):F2} MB.");

            MessageBox.Show(resumen.ToString(), "Limpieza del Disco Finalizada");
        }

        private long LimpiarCacheMiniaturas(ref int archivosBorrados)
        {
            string cachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows", "Explorer");
            long espacioLiberado = 0;

            if (Directory.Exists(cachePath))
            {
                foreach (string file in Directory.GetFiles(cachePath, "thumbcache_*.db"))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        espacioLiberado += fileInfo.Length;
                        File.Delete(file);
                        archivosBorrados++;
                    }
                    catch { /*Archivo en uso o no accesible*/ }
                }
            }
            return espacioLiberado;
        }

        private long LimpiarReportesError(ref int archivosBorrados)
        {
            string reportsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Windows\WER");
            long espacioLiberado = 0;

            if (Directory.Exists(reportsPath))
            {
                var directory = new DirectoryInfo(reportsPath);

                foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
                {
                    try
                    {
                        espacioLiberado += file.Length;
                        file.Delete();
                        archivosBorrados++;
                    }
                    catch { /*Archivo en uso o no accesible*/ }
                }
            }
            return espacioLiberado;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NvidiaPage());
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
