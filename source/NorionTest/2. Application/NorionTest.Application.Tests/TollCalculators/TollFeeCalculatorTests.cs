using System;
using FluentAssertions;
using NorionTest.Application.TollCalculators;
using Xunit;

public class TollFeeCalculatorTests
{
    [Theory]
    [InlineData("2024-01-01 06:00:00", 8)]
    [InlineData("2024-01-01 06:29:59", 8)]
    [InlineData("2024-01-01 06:30:00", 13)]
    [InlineData("2024-01-01 06:59:59", 13)]
    [InlineData("2024-01-01 07:00:00", 18)]
    [InlineData("2024-01-01 07:59:59", 18)]
    [InlineData("2024-01-01 08:00:00", 13)]
    [InlineData("2024-01-01 08:29:59", 13)]
    [InlineData("2024-01-01 08:30:00", 8)]
    [InlineData("2024-01-01 14:59:59", 8)]
    [InlineData("2024-01-01 15:00:00", 13)]
    [InlineData("2024-01-01 15:29:59", 13)]
    [InlineData("2024-01-01 15:30:00", 18)]
    [InlineData("2024-01-01 16:00:00", 18)]
    [InlineData("2024-01-01 16:59:59", 18)]
    [InlineData("2024-01-01 17:00:00", 13)]
    [InlineData("2024-01-01 17:59:59", 13)]
    [InlineData("2024-01-01 18:00:00", 8)]
    [InlineData("2024-01-01 18:29:59", 8)]
    [InlineData("2024-01-01 18:30:00", 0)]
    [InlineData("2024-01-01 23:59:59", 0)]
    public void CalculateTollFee_ShouldReturnExpectedFee(DateTime date, int expectedFee)
    {
        var tollFeeCalculator = new TollFeeCalculator();
        
        var result = tollFeeCalculator.CalculateTollFee(date);

        result.Should().Be(expectedFee);
    }
}