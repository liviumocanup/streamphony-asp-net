using AutoMapper;
using Streamphony.Application.Abstractions.Mapping;

namespace Streamphony.Infrastructure.Mapping;

public class AutoMapperProvider(IMapper mapper) : IMappingProvider
{
    private readonly IMapper _mapper = mapper;

    public TDestination Map<TDestination>(object? source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }
}