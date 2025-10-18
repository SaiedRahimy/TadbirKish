using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TadbirKish.DataReception.Application.Common.Interfaces;

namespace TadbirKish.DataReception.Infrastructure.Services
{
    public class CacheCoverageHostedService : IHostedService
    {
        private  ICacheCoverageManager _cacheCoverageManager;
        private readonly IServiceProvider _services;
        private readonly System.Timers.Timer CacheTimer;

        #region Constructor
        public CacheCoverageHostedService(IServiceProvider services)
        {
            _services= services;

            CacheTimer = new System.Timers.Timer(TimeSpan.FromMinutes(15));
            CacheTimer.Elapsed += async (sender, e) =>
            {
                await _cacheCoverageManager.FillCoveragesCache();
            };
            CacheTimer.Enabled = true;

        }
        #endregion

        #region IHostedService implementation

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cacheCoverageManager = _services.CreateScope().ServiceProvider.GetRequiredService<ICacheCoverageManager>(); ;

            await _cacheCoverageManager.FillCoveragesCache();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        #endregion
    }
}
