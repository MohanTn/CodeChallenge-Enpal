using System;

namespace Enpal.AppointmentBooking.Application.Dtos;

public class CalendarQueryRequestDto
{
    public required DateTime Date { get; set; }
    public required List<string> Products { get; set; }
    public required string Language { get; set; }
    public required string Rating { get; set; }
}
