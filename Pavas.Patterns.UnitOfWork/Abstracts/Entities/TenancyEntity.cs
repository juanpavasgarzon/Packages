using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class TenancyEntity : ITenancy
{
    [MaxLength(255)] public string TenantId { get; set; } = string.Empty;
}