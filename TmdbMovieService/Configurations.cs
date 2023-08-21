namespace TmdbMovieService
{
    public class Configurations
    {
        private static ConfigurationManager Configuration
        {
            get
            {
                ConfigurationManager configurationManager = new();

                configurationManager.SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");

                return configurationManager;
            }
        }

        public static string AppConnectionString => Configuration.GetConnectionString("AppDbcontext") ?? throw new Exception("Connection string bulunamadı.");
        public static string HangfireConnectionString => Configuration.GetConnectionString("HangfireConnectionString") ?? throw new Exception("Connection string bulunamadı.");
        public static string GetMoviesCronExpression => Configuration.GetSection("CronExpressions:GetMoviesCronExpression").Value ?? throw new Exception("GetMovies için CronExpressions belirtilmedi");
    }
}
