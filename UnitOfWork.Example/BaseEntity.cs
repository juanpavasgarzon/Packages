using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public abstract class BaseEntity : ITimestamps, ITenancy, ISoftDelete, ICorrelated
{
    [Timestamp] public DateTime CreatedAt { get; set; }
    [Timestamp] public DateTime? UpdatedAt { get; set; }
    [MaxLength(255)] public string TenantId { get; set; } = string.Empty;
    [DefaultValue(false)] public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    [MaxLength(255)] public string CorrelationId { get; set; } = string.Empty;
}