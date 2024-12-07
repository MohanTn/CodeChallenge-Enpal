using Enpal.AppointmentBooking.Application.Dtos;
using Enpal.AppointmentBooking.Application.Services;
using Enpal.AppointmentBooking.Core.Entities;
using Enpal.AppointmentBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class CalendarQueryServiceTests
{
    private readonly CalendarQueryService _service = default;

    public CalendarQueryServiceTests()
    {
        var option = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(option);

        var salesManager = new SalesManager
        {
            Id = 1,
            Name = "TestSalesManager2",
            Languages = new List<string> { "English" },
            CustomerRatings = new List<string> { "Gold" },
            Products = new List<string> { "SolarPanels" },
        };

        var slot1 = new Slot
        {
            Id = 1,
            StartDate = new DateTimeOffset(new DateTime(2024, 12, 8, 10, 0, 0), TimeSpan.Zero),
            EndDate = new DateTimeOffset(new DateTime(2024, 12, 8, 11, 0, 0), TimeSpan.Zero),
            Booked = false,
            SalesManagerId = 1,
        };

        var slot2 = new Slot
        {
            Id = 2,
            StartDate = new DateTimeOffset(new DateTime(2024, 12, 8, 11, 0, 0), TimeSpan.Zero),
            EndDate = new DateTimeOffset(new DateTime(2024, 12, 8, 12, 0, 0), TimeSpan.Zero),
            Booked = true,
            SalesManagerId = 1,
        };

        context.SalesManager.Add(salesManager);
        context.Slot.AddRange(slot1, slot2);
        context.SaveChanges();

        _service = new CalendarQueryService(context);
    }

    [Fact]
    public void GetFreeSlot_ReturnsCorrectSlots_WhenValidDataIsProvided()
    {
        // Arrange
        var requestDto = new CalendarQueryRequestDto
        {
            Language = "English",
            Rating = "Gold",
            Products = new List<string> { "SolarPanels" },
            Date = new DateTime(2024, 12, 8),
        };

        // Act
        var result = _service.GetFreeSlot(requestDto);

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.First().AvailableCount);
        Assert.Equal(new DateTime(2024, 12, 8, 10, 0, 0), result.First().StartDate.DateTime);
    }

    [Fact]
    public void GetFreeSlot_ReturnsNoSlots_WhenAllSlotsAreBooked()
    {
        var requestDto = new CalendarQueryRequestDto
        {
            Language = "German",
            Rating = "Gold",
            Products = new List<string> { "SolarPanels" },
            Date = new DateTime(2024, 12, 8),
        };

        // Act
        var result = _service.GetFreeSlot(requestDto);

        // Assert
        Assert.Empty(result);
    }
}
