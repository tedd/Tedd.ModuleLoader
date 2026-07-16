using System;
using System.Threading;
using System.Threading.Tasks;
using Tedd.ModuleLoader.Test.Common;

namespace Tedd.ModuleLoader.Test.Module1
{
    public class HostedModule : IHostedModule
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
