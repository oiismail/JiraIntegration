namespace JiraAPI.Model.Common
{
    public class AppConfiguration
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _JiraURL = string.Empty;
        private readonly string _JiraUsername = string.Empty;
        private readonly string _JiraToken = string.Empty;
        public AppConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            IConfigurationRoot root = configurationBuilder.Build();

            _connectionString = root.GetConnectionString("SqlConnection");
            _JiraURL = root.GetSection("Jira").GetSection("URL").Value;
            _JiraUsername = root.GetSection("Jira").GetSection("Username").Value;
            _JiraToken = root.GetSection("Jira").GetSection("Token").Value;
        }
        public string ConnectionString
        {
            get => _connectionString;
        }
        public string JiraURL
        {
            get => _JiraURL;
        }
        public string JiraUsername
        {
            get => _JiraUsername;
        }
        public string JiraToken
        {
            get => _JiraToken;
        }
    }
}
