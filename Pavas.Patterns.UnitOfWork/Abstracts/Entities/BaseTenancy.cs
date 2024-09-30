using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts.Entities;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class BaseTenancy : ITenancy
{
    [MaxLength(255)] public string TenantId { get; set; } = string.Empty;
}