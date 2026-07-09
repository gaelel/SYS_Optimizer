using OptimizationAPP.Properties;
using OptimizationAPP.Utilitys;
using System.Windows;

namespace OptimizationAPP
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                if (LanguageManager.HasSavedLanguage())
                {
                    LanguageManager.LoadSavedLanguage();
                    new MainWindow().Show();
                }
                else
                {
                    new LanguageSelectorWindow().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nInner: {ex.InnerException?.Message}\n\nStack: {ex.StackTrace}");
            }
        }
    }
}
