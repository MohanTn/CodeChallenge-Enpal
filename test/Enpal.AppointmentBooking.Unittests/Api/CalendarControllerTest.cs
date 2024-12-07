using AutoMapper;
using Enpal.AppointmentBooking.Api.Controller;
using Enpal.AppointmentBooking.Api.Models.SlotQuery;
using Enpal.AppointmentBooking.Application.Dtos;
using Enpal.AppointmentBooking.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class CalendarControllerTests
{
    private readonly Mock<ICalendarQueryService> _mockCalendarQueryService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CalendarController _controller;

    public CalendarControllerTests()
    {
        _mockCalendarQueryService = new Mock<ICalendarQueryService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new CalendarController(_mockCalendarQueryService.Object, _mockMapper.Object);
    }

    [Fact]
    public void Query_ReturnsOkResult_WithValidData()
    {
        // Arrange
        var request = new CalendarQueryRequest
        {
            Date = new DateTime(year: 2024, month: 12, day: 12),
            Language = "English",
            Rating = "Gold",
            Products = new List<string> { "SolarPanels" },
        };

        var requestDto = new CalendarQueryRequestDto
        {
            Date = request.Date,
            Language = request.Language,
            Rating = request.Rating,
            Products = request.Products,
        };

        var freeSlots = new List<CalendarQueryResponseDto>
        {
            new CalendarQueryResponseDto { AvailableCount = 1, StartDate = DateTime.UtcNow },
        };

        var response = new List<CalendarQueryResponse>
        {
            new CalendarQueryResponse { AvailableCount = 1, StartDate = DateTime.UtcNow },
        };

        _mockMapper
            .Setup(m => m.Map<CalendarQueryRequestDto>(It.IsAny<CalendarQueryRequest>()))
            .Returns(requestDto);
        _mockMapper
            .Setup(m =>
                m.Map<List<CalendarQueryResponse>>(It.IsAny<List<CalendarQueryResponseDto>>())
            )
            .Returns(response);

        _mockCalendarQueryService
            .Setup(s => s.GetFreeSlot(It.IsAny<CalendarQueryRequestDto>()))
            .Returns(freeSlots);

        // Act
        var result = _controller.Query(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CalendarQueryResponse>>(okResult.Value);
        Assert.Equal(freeSlots.Count, returnValue.Count);
    }
}
