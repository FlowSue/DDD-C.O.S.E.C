using Microsoft.Extensions.Configuration;

namespace C.O.S.E.C.Infrastructure.Config
{
    public class TestConfigModel
    {

        private readonly IConfigurationSection _config;

        public TestConfigModel(IConfiguration configuration)
        {
            _config = configuration.GetSection("Test");
        }

        public string Key1 => _config.GetValue("Key1", "");
        public string Key2 => _config.GetValue("Key2", "");
    }
}