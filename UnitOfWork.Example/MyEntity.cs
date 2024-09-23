using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace UnitOfWork.Example;

public class MyEntity : ITimestamps, ITenancy, ISoftDelete
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Timestamp] public DateTime CreatedAt { get; set; }

    [Timestamp] public DateTime? UpdatedAt { get; set; }

    [MaxLength(255)] public string TenantId { get; set; } = string.Empty;

    [DefaultValue(false)] public bool IsDeleted { get; set; }
    [Timestamp] public DateTime? DeletedAt { get; set; }
}