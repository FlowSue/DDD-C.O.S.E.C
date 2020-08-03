using Microsoft.Extensions.Configuration;

namespace C.O.S.E.C.Infrastructure.Config
{
    public class ConnectionStringsModel
    {

        private readonly IConfigurationSection _configSection;

        public ConnectionStringsModel(IConfiguration configuration)
        {
            _configSection = configuration.GetSection("ConnectionStrings");
        }
        public string SqlServerDatabase => _configSection.GetValue("SqlServerDatabase", string.Empty);
    }
}