using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts.Entities;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class BaseCorrelated : ICorrelated
{
    [MaxLength(255)] public string CorrelationId { get; set; } = string.Empty;
}