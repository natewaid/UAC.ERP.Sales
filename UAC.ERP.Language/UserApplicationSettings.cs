namespace UAC.ERP.Language
{
    public sealed class UserApplicationSettings
    {
        private UserApplicationSettings()
        {
        }

        private static UserApplicationSettings instance = null;

        public static UserApplicationSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserApplicationSettings();
                }

                return instance;
            }
        }

        public string SelectedDB { get; set; }

        public string SelectedCulture { get; set; }

        public string SelectedLanguage { get; set; }

        public string SelectedPlant { get; set; }
    }
}
