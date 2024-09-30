using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class CorrelatedEntity : ICorrelated
{
    [MaxLength(255)] public string CorrelationId { get; set; } = string.Empty;
}