using System;

namespace Enpal.AppointmentBooking.Application.Dtos;

public class CalendarQueryResponseDto
{
    public int AvailableCount { get; set; }
    public DateTimeOffset StartDate { get; set; }
}
