using Microsoft.Extensions.DependencyInjection;
using Things.Persistence;

namespace Persistence;

public static class Startup
{
    public static IServiceCollection AddFileSystemThingRepository(this IServiceCollection services, DirectoryInfo storageLocation) => 
        services.AddSingleton<IThingRepository>(new FileSystemThingRepository(storageLocation));
}
