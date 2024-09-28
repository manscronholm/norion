using FluentAssertions;
using NorionTest.Application.Toll.Interfaces;
using NorionTest.Application.Toll.TollCalculators;
using NorionTest.Application.Toll.TollCalculators.Interfaces;
using NorionTest.Domain;
using NSubstitute;

namespace NorionTest.Application.Tests.Toll.TollCalculators;

public class TollCalculatorTests
{
    private readonly ITollFeeCalculator _tollFeeCalculator;
    private readonly ITollFreeDateEvaluator _tollFreeDateEvaluator;
    private readonly TollCalculator _tollCalculator;

    public TollCalculatorTests()
    {
        _tollFeeCalculator = Substitute.For<ITollFeeCalculator>();
        _tollFreeDateEvaluator = Substitute.For<ITollFreeDateEvaluator>();
        _tollCalculator = new TollCalculator(_tollFeeCalculator, _tollFreeDateEvaluator);
    }

    [Fact]
    public void GetTollFee_WithMultipleDatesInSameHour_ShouldAggregateFees()
    {
        var vehicle = new Car();
        var dates = new[]
        {
            new DateTime(2023, 7, 1, 8, 0, 0),
            new DateTime(2023, 7, 1, 12, 00, 0),
            new DateTime(2023, 7, 1, 16, 00, 0)
        };

        _tollFreeDateEvaluator.IsTollFreeDate(Arg.Any<DateTime>()).Returns(false);
        
        _tollFeeCalculator.CalculateTollFee(dates[0]).Returns(10);
        _tollFeeCalculator.CalculateTollFee(dates[1]).Returns(10);
        _tollFeeCalculator.CalculateTollFee(dates[2]).Returns(10);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(30);
    }

    [Fact]
    public void GetTollFee_WithDatesMoreThanAnHourApart_ShouldCountEachFee()
    {
        var vehicle = new Car();
        var dates = new[]
        {
            new DateTime(2023, 7, 1, 10, 1, 0),
            new DateTime(2023, 7, 1, 11, 2, 0),
            new DateTime(2023, 7, 1, 12, 3, 0)
        };

        _tollFreeDateEvaluator.IsTollFreeDate(Arg.Any<DateTime>()).Returns(false);
        
        _tollFeeCalculator.CalculateTollFee(dates[0]).Returns(10);
        _tollFeeCalculator.CalculateTollFee(dates[1]).Returns(20);
        _tollFeeCalculator.CalculateTollFee(dates[2]).Returns(30);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(60); 
    }

    [Fact]
    public void GetTollFee_WithNoDates_ShouldReturnZero()
    {
        var vehicle = new Car();
        var dates = Array.Empty<DateTime>();

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(0);
    }

    [Fact]
    public void GetTollFee_WithSingleDate_ShouldReturnFee()
    {
        var vehicle = new Car();
        var date = new DateTime(2023, 7, 1, 10, 0, 0);
        var dates = new[] { date };

        _tollFreeDateEvaluator.IsTollFreeDate(date).Returns(false);
        _tollFeeCalculator.CalculateTollFee(date).Returns(10);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(10);
    }

    [Fact]
    public void GetTollFee_WithTollFreeDate_ShouldReturnZero()
    {
        var vehicle = new Car();
        var date = new DateTime(2023, 7, 1);
        var dates = new[] { date };

        _tollFreeDateEvaluator.IsTollFreeDate(date).Returns(true);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(0);
    }

    [Fact]
    public void GetTollFee_WithTollFreeVehicle_ShouldReturnZero()
    {
        var vehicle = new Motorbike();
        var date = new DateTime(2023, 7, 1);
        var dates = new[] { date };

        _tollFreeDateEvaluator.IsTollFreeDate(date).Returns(false);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(0);
    }

    [Fact]
    public void GetTollFee_WithFeesExceedingSixty_ShouldCapAtSixty()
    {
        var vehicle = new Car();
        var dates = new[]
        {
            new DateTime(2023, 7, 1, 10, 0, 0),
            new DateTime(2023, 7, 1, 12, 0, 0),
            new DateTime(2023, 7, 1, 14, 0, 0),
            new DateTime(2023, 7, 1, 16, 0, 0)
        };

        _tollFreeDateEvaluator.IsTollFreeDate(Arg.Any<DateTime>()).Returns(false);
        
        _tollFeeCalculator.CalculateTollFee(dates[0]).Returns(20);
        _tollFeeCalculator.CalculateTollFee(dates[1]).Returns(20);
        _tollFeeCalculator.CalculateTollFee(dates[2]).Returns(20);
        _tollFeeCalculator.CalculateTollFee(dates[3]).Returns(20);

        var result = _tollCalculator.GetTollFee(vehicle, dates);

        result.Should().Be(60);
    }
}