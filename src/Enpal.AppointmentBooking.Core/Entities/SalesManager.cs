using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enpal.AppointmentBooking.Core.Entities;

public class SalesManager
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(250)]
    [Column("name")]
    public required string Name { get; set; }

    [Column("languages", TypeName = "character varying[]")]
    public required List<string> Languages { get; set; }

    [Column("products", TypeName = "character varying[]")]
    public required List<string> Products { get; set; }

    [Column("customer_ratings", TypeName = "character varying[]")]
    public required List<string> CustomerRatings { get; set; }
    public ICollection<Slot> Slots { get; set; }
}
