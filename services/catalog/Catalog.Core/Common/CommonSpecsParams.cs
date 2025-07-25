namespace Catalog.Core.Common;

public class CommonSpecsParams
{
    private int _pageSize = 10;
    private const int MaxPageSize = 70;

    //TODO
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int PageIndex { get; set; } = 1;
    public string Sort { get; set; }
    public string Search { get; set; }
}