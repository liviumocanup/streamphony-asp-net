using Mapster;
using Streamphony.Infrastructure.Mapping.Profiles;

namespace Streamphony.Infrastructure.Mapping;

public class MapsterMapper : Application.Abstractions.Mapping.IMapper
{
    private readonly TypeAdapterConfig _config;

    public MapsterMapper()
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