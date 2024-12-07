using System;

namespace Enpal.AppointmentBooking.Api.Models.SlotQuery;

public class CalendarQueryRequest
{
    public required DateTime Date { get; set; }
    public required List<string> Products { get; set; }
    public required string Language { get; set; }
    public required string Rating { get; set; }
}
