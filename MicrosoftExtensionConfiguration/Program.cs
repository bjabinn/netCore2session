using Microsoft.Extensions.Configuration;
using System;

namespace MicrosoftExtensionConfiguration
{
    class Program
    {
        public static void Main(string[] args)
        {
            //JSON
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            foreach (var item in configuration.GetChildren())
            {
                Visit(item);
            }

            Console.WriteLine("Acceso a un elemento directamente: " + configuration["Logging:LogLevel:Default"]);
            Console.Read();

            //INI
            var builder2 = new ConfigurationBuilder().AddIniFile("file.INI");
            var configuration2 = builder2.Build();
            var result2 = configuration2.GetSection("Data:ConnectionString");

            //XML
            var builder3 = new ConfigurationBuilder().AddXmlFile("appsettings.xml");
            var configuration3 = builder3.Build();
            //var result3 = configuration3.Get<string>("Data.Setting:DefaultConnection:Connection.String");


        }

        static void Visit(IConfigurationSection section)
        {
            Console.WriteLine("Name:{0} Path:{1}", section.Key, section.Path);

            foreach (var item in section.GetChildren())
            {
                Visit(item);
            }
        }

    }
}
