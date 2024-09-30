using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class SoftDeleteEntity : ISoftDelete
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}