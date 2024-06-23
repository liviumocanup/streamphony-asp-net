using Streamphony.Application.Common.Enum;

namespace Streamphony.Application.Common;

public class Filter
{
    public string Path { get; set; } = default!;
    public string Value { get; set; } = default!;
    public FilterValueType ValueType { get; set; } = FilterValueType.String;
}
