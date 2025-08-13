using System.Runtime.InteropServices.JavaScript;

namespace Ordering.Core.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
    public int Version { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}