namespace Streamphony.Application.Abstractions.Mapping;

public interface IMappingProvider
{
    TDestination Map<TDestination>(object? source);
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
}
