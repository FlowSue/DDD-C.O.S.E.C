using C.O.S.E.C.Infrastructure.Config.BusinessConfigModel;
using C.O.S.E.C.Infrastructure.Config.FrameConfigModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Config
{
    public class AllConfigModel
    {
        private readonly IConfiguration _configuration;

        public AllConfigModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// 认证授权配置
        /// </summary>
        public JwtAuthConfigModel JwtAuthConfigModel => new JwtAuthConfigModel(_configuration);

        /// <summary>
        /// 连接字符串配置
        /// </summary>
        public ConnectionStringsModel ConnectionStringsModel => new ConnectionStringsModel(_configuration);

        public TestConfigModel TestConfigModel => new TestConfigModel(_configuration);


        public APIConfigModel APIConfigModel => new APIConfigModel(_configuration);
    }
}
