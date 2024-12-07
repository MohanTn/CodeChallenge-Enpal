using Enpal.AppointmentBooking.Application.Dtos;
using Enpal.AppointmentBooking.Application.Interface;
using Enpal.AppointmentBooking.Core.Entities;
using Enpal.AppointmentBooking.Core.Interface;

namespace Enpal.AppointmentBooking.Application.Services;

public class CalendarQueryService : ICalendarQueryService
{
    public CalendarQueryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public readonly IApplicationDbContext _context;

    public IEnumerable<CalendarQueryResponseDto> GetFreeSlot(CalendarQueryRequestDto requestDto)
    {
        return _context
            .Set<Slot>()
            .Join(
                _context
                    .Set<SalesManager>()
                    .Where(manager =>
                        manager.Languages.Contains(requestDto.Language)
                        && manager.CustomerRatings.Contains(requestDto.Rating)
                        && !requestDto.Products.Except(manager.Products).Any()
                    ),
                slot => slot.SalesManagerId,
                manager => manager.Id,
                (slot, manager) => new { slot, manager }
            )
            .Where(sm =>
                sm.slot.StartDate.Date == requestDto.Date
                && !_context
                    .Set<Slot>()
                    .Any(booking =>
                        booking.SalesManagerId == sm.manager.Id
                        && booking.Booked
                        && (
                            booking.StartDate < sm.slot.EndDate
                            && booking.EndDate > sm.slot.StartDate
                        )
                    )
            )
            .GroupBy(sm => new { sm.slot.StartDate, sm.slot.EndDate })
            .Select(group => new CalendarQueryResponseDto
            {
                AvailableCount = group.Count(),
                StartDate = group.Key.StartDate,
            })
            .OrderBy(sm => sm.StartDate)
            .ToList();
    }
}
