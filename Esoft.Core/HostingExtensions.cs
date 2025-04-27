using ESoft.Core.Logging;
using Microsoft.Extensions.Logging;

namespace Esoft.Core
{
    public static class HostingExtensions
    {
        public static void ConfigureExtentionsService(this ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddProvider(new EsoftLog());

            //TODO: config rate limit
        }
    }
}
