namespace Streamphony.Infrastructure.Mapping;

public class AutoMapperService(AutoMapper.IMapper mapper) : Application.Abstractions.Mapping.IMapper
{
    private readonly AutoMapper.IMapper _mapper = mapper;

    public TDestination Map<TDestination>(object? source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }
}