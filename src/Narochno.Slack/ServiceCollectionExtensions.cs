using Microsoft.Extensions.DependencyInjection;

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
            return services.AddSingleton<ISlackClient>(new SlackClient(config));
        }
    }
}
