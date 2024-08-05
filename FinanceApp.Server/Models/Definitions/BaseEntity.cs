using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Server.Models.Definitions;

public class BaseEntity
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.Now;

    public DateTime DateUpdated { get; set; } = DateTime.Now;
}
