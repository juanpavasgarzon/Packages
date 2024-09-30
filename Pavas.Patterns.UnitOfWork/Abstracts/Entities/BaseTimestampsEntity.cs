using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts.Entities;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class BaseTimestampsEntity : ITimestamps
{
    [Timestamp] public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}