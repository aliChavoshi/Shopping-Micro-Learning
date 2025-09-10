using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging;

public class Logging
{
    //Configuration for all of the errors and logs
    public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, loggerConfiguration) =>
    {

    };
}
