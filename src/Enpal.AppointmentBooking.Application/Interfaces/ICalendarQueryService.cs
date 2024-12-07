using System;
using Enpal.AppointmentBooking.Application.Dtos;

namespace Enpal.AppointmentBooking.Application.Interface;

public interface ICalendarQueryService
{
    IEnumerable<CalendarQueryResponseDto> GetFreeSlot(CalendarQueryRequestDto requestDto);
}
