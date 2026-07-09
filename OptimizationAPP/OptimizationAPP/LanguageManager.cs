using OptimizationAPP.Properties;
using System.Windows;

namespace OptimizationAPP.Utilitys
{
    public static class LanguageManager
    {

        public static void SetLanguage(string languageCode)
        {
            if (languageCode.StartsWith("en")) languageCode = "en";
            if (languageCode.StartsWith("es")) languageCode = "es";

            string ruta = languageCode == "es"
                ? "pack://application:,,,/Resources/Lang.es.xaml"
                : "pack://application:,,,/Resources/Lang.en.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri(ruta, UriKind.Absolute)
            };

            var existing = FindLanguageDictionary();
            if (existing != null) Application.Current.Resources.MergedDictionaries.Remove(existing);

            Application.Current.Resources.MergedDictionaries.Add(dict);

            Settings.Default.Language = languageCode;
            Settings.Default.Save();
        }

        public static string Get(string key)
        {
            if (Application.Current.Resources.Contains(key)) return Application.Current.Resources[key] as string ?? key;
            return key;
        }

        public static bool HasSavedLanguage()
        {
            return !string.IsNullOrEmpty(Settings.Default.Language);
        }

        public static void LoadSavedLanguage()
        {
            SetLanguage(Settings.Default.Language);
        }

        private static ResourceDictionary FindLanguageDictionary()
        {
            foreach (var dict in Application.Current.Resources.MergedDictionaries)
            {
                if (dict.Source != null && (dict.Source.OriginalString.Contains("Lang.es") || dict.Source.OriginalString.Contains("Lang.en")))
                    return dict;
            }
            return null;
        }
    }
}
