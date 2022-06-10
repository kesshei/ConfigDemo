using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigDemo
{
    /// <summary>
    /// 配置源
    /// </summary>
    public class ConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConfigProvider();
        }
    }
}
