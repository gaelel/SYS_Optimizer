using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationAPP.Pages
{
    public partial class EventVwrPage : Page
    {
        private readonly string[] _registrosObjetivo = {"Application",
                                                        "Security",
                                                        "Setup",
                                                        "System"};

        public EventVwrPage()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            var resumen = new System.Text.StringBuilder();
            int registrosLimpiados = 0;
            int registrosFallidos = 0;

            foreach (string registro in _registrosObjetivo)
            {
                try
                {
                    using (var eventLog = new EventLog(registro))
                    {
                        eventLog.Clear();
                        registrosLimpiados++;
                        resumen.AppendLine($"Registro '{registro}' limpiado correctamente.");
                    }
                }
                catch (Exception  ex)
                {
                    registrosFallidos++;
                    resumen.AppendLine($"No se pudo limpiar el registro '{registro}': {ex.Message}");
                }
            }

            resumen.AppendLine();
            resumen.AppendLine($"Se limpiaron {registrosLimpiados} registro(s) de {_registrosObjetivo.Length} correctamente.");

            if (registrosFallidos > 0) resumen.AppendLine($"{registrosFallidos} registro(s) no pudieron ser limpiados.");

            MessageBox.Show(resumen.ToString(), "Limpieza de Registros Completada");
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdvDiskPage());
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
