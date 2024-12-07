using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enpal.AppointmentBooking.Core.Entities;

public class Slot
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("start_date")]
    public DateTimeOffset StartDate { get; set; }

    [Column("end_date")]
    public DateTimeOffset EndDate { get; set; }

    [Column("booked")]
    public bool Booked { get; set; }

    [Column("sales_manager_id")]
    public int SalesManagerId { get; set; }
    public SalesManager SalesManager { get; set; }
}
