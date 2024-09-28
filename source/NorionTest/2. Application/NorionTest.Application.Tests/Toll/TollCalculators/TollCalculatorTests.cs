using FluentAssertions;
using NorionTest.Application.Toll.Models;
using NorionTest.Application.Toll.TollCalculators;
using NorionTest.Domain;
using NorionTest.Domain.Interfaces;
using NSubstitute;

namespace NorionTest.Application.Tests.Toll.TollCalculators;

public class TollCalculatorTests
{
    [Theory]
    [InlineData(TollFreeVehicles.Motorbike)]
    [InlineData(TollFreeVehicles.Tractor)]
    [InlineData(TollFreeVehicles.Emergency)]
    [InlineData(TollFreeVehicles.Diplomat)]
    [InlineData(TollFreeVehicles.Foreign)]
    [InlineData(TollFreeVehicles.Military)]
    public void GetTollFee_WithTollFreeVehicle_ShouldReturnZero(TollFreeVehicles tollFreeVehicle)
    {
        var vehicleMock = Substitute.For<IVehicle>();
        vehicleMock.GetVehicleType().Returns(Enum.GetName(typeof(TollFreeVehicles), tollFreeVehicle));
        var sut = new TollCalculator(null, null);

        var result = sut.GetTollFee(vehicleMock, GetNonTollFreeDates());

        result.Should().Be(0);
    }

    [Fact]
    public void GetTollFee_WithNonTollFreeVehicle_ShouldReturnFee()
    {
        var car = new Car();
        var sut = new TollCalculator(null, null);

        var result = sut.GetTollFee(car, GetNonTollFreeDates());

        result.Should().BePositive(null);
    }

    [Fact]
    public void GetTollFee_DuringTollFreeDate_ShouldReturnZero()
    {
        var car = new Car();
        var sut = new TollCalculator(null, null);

        var result = sut.GetTollFee(car, GetTollFreeDates());

        result.Should().Be(0);
    }

    [Fact]
    public void GetTollFee_DuringNonTollFreeDate_ShouldReturnFee()
    {
        var car = new Car();
        var sut = new TollCalculator(null, null);

        var result = sut.GetTollFee(car, GetNonTollFreeDates());

        result.Should().BePositive();
    }

    private static DateTime[] GetNonTollFreeDates()
    {
        var nonTollFreeTime = new TimeOnly(7, 0);
        return
        [
            new DateTime(new DateOnly(2024, 01, 02), nonTollFreeTime),
            new DateTime(new DateOnly(2024, 01, 03), nonTollFreeTime),
            new DateTime(new DateOnly(2024, 01, 04), nonTollFreeTime) 
        ];
    }

    private static DateTime[] GetTollFreeDates() =>
    [
        new(2013, 12, 24),
        new(2013, 12, 25),
        new(2013, 12, 26)
    ];
}