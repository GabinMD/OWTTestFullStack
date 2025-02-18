namespace BoatApplication.Domain.Common.Entities;
public abstract class BaseAuditableEntity : BaseDomainEntity
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
