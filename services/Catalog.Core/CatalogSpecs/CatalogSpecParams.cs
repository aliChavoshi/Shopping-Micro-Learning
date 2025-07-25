using Catalog.Core.Common;

namespace Catalog.Core.CatalogSpecs;

public class CatalogSpecParams : CommonSpecsParams
{
    public string BrandId { get; set; }
    public string TypeId { get; set; }
}