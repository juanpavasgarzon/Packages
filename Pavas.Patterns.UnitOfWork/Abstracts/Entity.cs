using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts;

public abstract class Entity : IEntity
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(255)]
    public string TenantId { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Timestamp] public DateTime? UpdatedAt { get; set; } = null;
}