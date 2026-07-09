using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationAPP.Pages
{
    public partial class AdvDiskPage : Page
    {
        public AdvDiskPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            var resumen = new System.Text.StringBuilder();
            long espacioLiberado = 0;

            int archivosActualizaciones = 0;
            long espacioActualizaciones = LimpiarActualizaciones(ref archivosActualizaciones);
            espacioLiberado += espacioActualizaciones;
            resumen.AppendLine($"Se eliminaron {archivosActualizaciones} archivos de actualizaciones, liberando {espacioActualizaciones / (1024.0 * 1024.0):F2} MB.");

            long espacioVersiones = LimpiarVersiones();
            espacioLiberado += espacioVersiones;
            resumen.AppendLine($"Se elimino la carpeta Windows.Old, liberando {espacioVersiones / (1024.0 * 1024.0):F2} MB.");

            int archivosLogs = 0;
            long espacioLogs = LimpiarLogs(ref archivosLogs);
            espacioLiberado += espacioLogs;
            resumen.AppendLine($"Se eliminaron {archivosLogs} archivos de logs, liberando {espacioLogs / (1024.0 * 1024.0):F2} MB.");

            resumen.AppendLine();
            resumen.AppendLine($"Total de espacio liberado: {espacioLiberado / (1024.0 * 1024.0):F2} MB.");

            MessageBox.Show(resumen.ToString(), "Limpieza Avanzada del Disco Finalizada");
        }

        private long CalcularTamanoCarpeta(string path)
        {
            long totalSize = 0;
            try
            {
                var directory = new DirectoryInfo(path);
                foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
                {
                    try{ totalSize += file.Length; }
                    catch { /* Archivo en uso */ }
                }
            }
            catch { }
            return totalSize;
        }

        private long LimpiarActualizaciones(ref int actualizacionesBorradas) 
        {
            string windowsUpdatePath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "SoftwareDistribution", "Download");
            long espacioLiberado = 0;

            if (!Directory.Exists(windowsUpdatePath)) return 0;

            var directory = new DirectoryInfo(windowsUpdatePath);

            foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                try
                {
                    espacioLiberado += file.Length;
                    file.Delete();
                    actualizacionesBorradas++;

                }
                catch { /* Archivo en uso */ }
            }

            foreach (var subDir in directory.GetDirectories())
            {
                try {subDir.Delete(true); }
                catch { /* Carpeta en uso */ }
            }
            return espacioLiberado;
        }
        private long LimpiarVersiones()
        {
            string windowsOldPath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "Windows.old");
            long espacioLiberado = 0;

            if (!Directory.Exists(windowsOldPath)) return 0;

            espacioLiberado = CalcularTamanoCarpeta(windowsOldPath);

            try { Directory.Delete(windowsOldPath, true); }
            catch{ }

            return espacioLiberado;
        }
        private long LimpiarLogs(ref int logsBorrados)
        {
            string logsPath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "Logs");
            long espacioLiberado = 0;

            if (!Directory.Exists(logsPath)) return 0;
            var directory = new DirectoryInfo(logsPath);

            foreach (var file in directory.GetFiles("*", SearchOption.AllDirectories))
            {
                try
                {
                    espacioLiberado += file.Length;
                    file.Delete();
                    logsBorrados++;

                }
                catch { /* Archivo en uso */ }
            }

            return espacioLiberado;
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
