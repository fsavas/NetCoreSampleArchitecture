using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading;

namespace Sample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File("./LogsFolder/log.txt")//todo get from DB . current directory
                .WriteTo.Console()
                .WriteTo.MSSqlServer(connectionString: "Data Source=11.11.10.162;Initial Catalog=Sample;User ID=docker;Password=docker", //"Data Source=.\\SQLExpress01;Initial Catalog=Sample;Integrated Security=True",
                    tableName: "Log", restrictedToMinimumLevel: LogEventLevel.Information,
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new Collection<SqlColumn>
                        {
                            new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "User"}
                        }
                    }
                )
                .CreateLogger();
            
            Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine(msg));

            try
            {
                Log.Information("Starting host");

                var host = CreateHostBuilder(args).Build();

                //ServiceContainer.Provider = host.Services;
                StartThread();

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ConnectionStrings__DevConnection")
                    ?? "Data Source=.\\SQLExpress01;Initial Catalog=Sample;Integrated Security=True";
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureWebHost(config =>
                {
                    config.UseUrls("http://*:63617");
                })
                .UseWindowsService()
                .UseSerilog();

        private static void StartThread()
        {
            ThreadPool.QueueUserWorkItem(RunThread);
        }

        private static void RunThread(object info)
        {
            int i = 0;

            while (true)
            {
                using (StreamWriter sw = File.AppendText("./LogsFolder/log.txt"))
                {
                    sw.WriteLine(i.ToString());
                }

                Thread.Sleep(1000);
                i++;
            }
        }
    }
}