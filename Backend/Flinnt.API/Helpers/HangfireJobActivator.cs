using Flinnt.Interfaces.Services;
using Flinnt.Services;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Flinnt.API.Helpers
{
    public class HangfireJobActivator : JobActivator
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HangfireJobActivator"/> class.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public HangfireJobActivator(IServiceCollection serviceCollection)
        {
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Activates the job.
        /// </summary>
        /// <param name="jobType">Type of the job.</param>
        /// <returns>object</returns>
        public override object ActivateJob(Type jobType)
        {
            return serviceProvider.GetService(jobType);
        }
    }
}
