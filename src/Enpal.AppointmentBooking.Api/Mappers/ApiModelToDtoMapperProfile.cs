using AutoMapper;
using Enpal.AppointmentBooking.Api.Models.SlotQuery;
using Enpal.AppointmentBooking.Application.Dtos;

namespace Enpal.AppointmentBooking.Api.Mappers;

public class ApiModelToDtoMapperProfile : Profile
{
    public ApiModelToDtoMapperProfile()
    {
        CreateMap<CalendarQueryRequest, CalendarQueryRequestDto>();
        CreateMap<CalendarQueryResponseDto, CalendarQueryResponse>();
    }
}
