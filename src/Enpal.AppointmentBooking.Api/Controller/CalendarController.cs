using System.Globalization;
using AutoMapper;
using Enpal.AppointmentBooking.Api.Mappers;
using Enpal.AppointmentBooking.Api.Models.SlotQuery;
using Enpal.AppointmentBooking.Application.Dtos;
using Enpal.AppointmentBooking.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Enpal.AppointmentBooking.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController(ICalendarQueryService calendarQueryService, IMapper mapper)
        : ControllerBase
    {
        public readonly ICalendarQueryService _calendarQueryService = calendarQueryService;
        public readonly IMapper _mapper = mapper;

        [HttpPost("query")]
        [ProducesResponseType(typeof(List<CalendarQueryResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Query(CalendarQueryRequest request)
        {
            var requestDto = _mapper.Map<CalendarQueryRequestDto>(request);
            var freeSlot = _calendarQueryService.GetFreeSlot(requestDto);

            // if (freeSlot.Count() == 0)
            // {
            //     return NotFound();
            // }
            var responce = _mapper.Map<List<CalendarQueryResponse>>(freeSlot);

            return Ok(responce);
        }
    }
}
