using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace ConfigDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();
            //直接读取相关配置信息
            var ossUrl = configuration.GetValue<string>("ossUrl");
            var serverUrl = configuration["serverUrl"];
            var Endpoint = configuration.GetValue<string>("AliOSSConfig:Endpoint");
            var AccessKeyId = configuration.GetSection("AliOSSConfig").GetSection("AccessKeyId").Value;
            var servers0 = configuration.GetValue<string>("servers:0");
            var servers1 = configuration.GetValue<string>("servers:1");
            var AliOSSConfigOjb = new AliOSSConfig();
            configuration.GetSection("AliOSSConfig").Bind(AliOSSConfigOjb);


            configBuilder.AddCustomConfig();
            configuration = configBuilder.Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<AliOSSConfig>(configuration.GetSection("AliOSSConfig"));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var _options = serviceProvider.GetRequiredService<IOptionsMonitor<AliOSSConfig>>();



            _options.OnChange(_ => Console.WriteLine(_.BucketName));
            //            Task.Run(() => {
            //=
            //                while (true)
            //                {
            //                    Thread.Sleep(2000);
            //                    Console.WriteLine(_options.CurrentValue.Name);
            //                }
            //            });
            Console.WriteLine("获取到配置信息!");
            Console.ReadLine();
        }
    }
}
