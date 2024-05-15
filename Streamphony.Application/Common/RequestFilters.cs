namespace Streamphony.Application.Common;

public class RequestFilters
{
    public FilterLogicalOperators LogicalOperator { get; set; }

    public IList<Filter> Filters { get; set; } = [];
}
