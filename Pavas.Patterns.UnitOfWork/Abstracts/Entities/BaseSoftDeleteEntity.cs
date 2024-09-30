using Pavas.Patterns.UnitOfWork.Contracts.Entities;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Entities;

public abstract class BaseSoftDeleteEntity : ISoftDelete
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
}