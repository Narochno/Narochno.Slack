using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Narochno.Slack
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the Slack client to the service collection as a singleton.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="config">The slack configuration.</param>
        /// <returns>The passed service collection.</returns>
        public static IServiceCollection AddSlack(this IServiceCollection services, SlackConfig config)
        {
            services.AddSingleton(config);
            services.AddHttpClient<ISlackClient, SlackClient>()
                .AddPolicyHandler(GetRetryPolicy(config));
            return services;
        }
        
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(SlackConfig config)
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode >= HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(config.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(config.RetryBackoffExponent, retryAttempt)));
        }
    }
}
