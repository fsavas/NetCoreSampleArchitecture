using Microsoft.Extensions.DependencyInjection;
using Sample.Services.BackgroundJobs.ScopedTasks;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Services.BackgroundJobs.HostedTasks
{
    public class SampleTask : BaseBackgroundService
    {
        #region Fields

        public IServiceProvider Services { get; }

        #endregion Fields

        #region Constructor

        public SampleTask(IServiceProvider services)
        {
            Services = services;
        }

        #endregion Constructor

        #region Methods

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Log.Logger.Information("ScopedSampleTask starting");

            await DoWork(cancellationToken);

            //await StopAsync(cancellationToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            Log.Logger.Information("ScopedSampleTask working");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Logger.Information("ScopedSampleTask stopping");

            await Task.CompletedTask;
        }

        #endregion Methods
    }
}