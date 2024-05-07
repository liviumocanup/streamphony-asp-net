using Mapster;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Infrastructure.Mapping.Profiles;

namespace Streamphony.Infrastructure.Mapping;

public class MapsterProvider : IMappingProvider
{
    private readonly TypeAdapterConfig _config;

    public MapsterProvider()
    {
        _config = MapsterConfig.GlobalConfig;
    }

    public TDestination Map<TDestination>(object? source)
    {
        return source.Adapt<TDestination>(_config);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return source.Adapt(destination, _config);
    }
}