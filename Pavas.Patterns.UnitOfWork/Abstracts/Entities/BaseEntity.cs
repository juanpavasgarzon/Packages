using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class BaseEntity : ITimestamps, ITenancy, ISoftDelete, ICorrelated
{
    [MaxLength(255)] public string TenantId { get; set; } = string.Empty;
    [Timestamp] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = null;
    [MaxLength(255)] public string CorrelationId { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}