using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigDemo
{
    /// <summary>
    /// 增加配置扩展
    /// </summary>
    public static class ConfigExtensions
    {
        /// <summary>
        /// 自定义扩展，增加配置 
        /// </summary>
        public static IConfigurationBuilder AddCustomConfig(this IConfigurationBuilder builder, IConfiguration configuration = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Add(new ConfigSource());
        }
    }
}
