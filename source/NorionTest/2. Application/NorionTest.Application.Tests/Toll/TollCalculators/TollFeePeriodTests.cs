using FluentAssertions;
using NorionTest.Application.Toll.TollCalculators.Models;

namespace NorionTest.Application.Tests.Toll.TollCalculators;

public class TollFeePeriodTests
{
    [Fact]
    public void IsWithinPeriod_ShouldReturnTrue_WhenTimeIsWithinStartAndEnd()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 26, 6, 15, 0); 

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsWithinPeriod_ShouldReturnFalse_WhenTimeIsBeforeStart()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 26, 5, 59, 0);

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsWithinPeriod_ShouldReturnFalse_WhenTimeIsAfterEnd()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 26, 6, 31, 0); 

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeFalse();
    }

    [Fact]
    public void IsWithinPeriod_ShouldReturnTrue_WhenTimeIsExactlyAtStart()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 26, 6, 0, 0); 

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsWithinPeriod_ShouldReturnTrue_WhenTimeIsExactlyAtEnd()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 26, 6, 30, 0);

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsWithinPeriod_ShouldReturnTrue_WhenTimeIsOnDifferentDay()
    {
        var timePeriod = new TollFeePeriod(new TimeOnly(6, 0), new TimeOnly(6, 30), 8);
        var testDateTime = new DateTime(2023, 9, 27, 6, 15, 0);

        var result = timePeriod.IsWithinPeriod(testDateTime);

        result.Should().BeTrue();
    }
}