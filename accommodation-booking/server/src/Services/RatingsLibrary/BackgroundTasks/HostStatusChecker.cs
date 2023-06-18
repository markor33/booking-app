using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RatingsLibrary.Repository;

namespace RatingsLibrary.BackgroundTasks
{
    public class HostStatusCheckTask : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HostStatusCheckTask(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var prominentHostRepository = scope.ServiceProvider.GetRequiredService<IProminentHostRepository>();
                    await prominentHostRepository.SetHasFiveReservationAcceptable();
                    await prominentHostRepository.SetDurationAcceptable();
                }

                await Task.Delay(TimeSpan.FromHours(3), stoppingToken);
            }
        }
    }
}
