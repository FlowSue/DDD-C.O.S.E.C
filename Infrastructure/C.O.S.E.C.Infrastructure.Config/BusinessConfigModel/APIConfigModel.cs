using Microsoft.Extensions.Configuration;

namespace C.O.S.E.C.Infrastructure.Config.BusinessConfigModel
{
    public class APIConfigModel
    {
        private readonly IConfiguration _configuration;

        public APIConfigModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}