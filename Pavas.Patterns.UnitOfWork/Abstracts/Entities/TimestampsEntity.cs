using System.ComponentModel.DataAnnotations;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class TimestampsEntity : ITimestamps
{
    [Timestamp] public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}