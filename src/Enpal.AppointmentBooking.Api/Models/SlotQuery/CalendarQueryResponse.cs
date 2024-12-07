namespace Enpal.AppointmentBooking.Api.Models.SlotQuery;

public class CalendarQueryResponse
{
    public int AvailableCount { get; set; }
    public DateTimeOffset StartDate { get; set; }
}
