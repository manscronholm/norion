﻿using FluentAssertions;
using NorionTest.Application.Toll;
using NorionTest.Application.Toll.TollCalculators;

namespace NorionTest.Application.Tests.Toll;

public class TollFreeDateEvaluatorTests
{
    
    [Theory]
    [InlineData(2013, 1, 1)]  
    [InlineData(2013, 3, 28)] 
    [InlineData(2013, 3, 29)] 
    [InlineData(2013, 4, 1)]  
    [InlineData(2013, 4, 30)] 
    [InlineData(2013, 5, 1)]  
    [InlineData(2013, 5, 8)]  
    [InlineData(2013, 5, 9)]  
    [InlineData(2013, 6, 5)]  
    [InlineData(2013, 6, 6)]  
    [InlineData(2013, 6, 21)] 
    [InlineData(2013, 11, 2)] 
    [InlineData(2013, 12, 24)] 
    [InlineData(2013, 12, 25)] 
    [InlineData(2013, 12, 26)] 
    [InlineData(2013, 12, 31)] 
    public void IsTollFreeDate_WithTollFreeDate_ShouldReturnTrue(int year, int month, int day)
    {
        var date = new DateTime(year, month, day);
        var sut = new TollFreeDateEvaluator();

        var result = sut.IsTollFreeDate(date);

        result.Should().BeTrue();
    }

    [Theory]
    // Saturday
    [InlineData(2024, 09, 28)]
    // Sunday
    [InlineData(2024, 09, 29)]
    public void IsTollFreeDate_WithDateDuringWeekend_ShouldReturnTrue(int year, int month, int day)
    {
        var date = new DateTime(year, month, day);
        var sut = new TollFreeDateEvaluator();

        var result = sut.IsTollFreeDate(date);

        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(2013, 1, 3)]  
    [InlineData(2013, 2, 1)] 
    [InlineData(2013, 3, 26)] 
    [InlineData(2013, 4, 3)]  
    public void IsTollFreeDate_WithNonTollFreeDate_ShouldReturnFalse(int year, int month, int day)
    {
        var date = new DateTime(year, month, day);
        var sut = new TollFreeDateEvaluator();

        var result = sut.IsTollFreeDate(date);

        result.Should().BeFalse();
    }
    
    [Theory]
    [MemberData(nameof(JulyDates))]
    public void IsTollFreeDate_WithDatesInJuly_ShouldReturnTrue(DateTime date)
    {
        var sut = new TollFreeDateEvaluator();

        var result = sut.IsTollFreeDate(date);

        result.Should().BeTrue();
    }
    
    public static IEnumerable<object[]> JulyDates()
    {
        for (var day = 1; day <= 31; day++)
        {
            var date = new DateTime(2013, 7, day);
            yield return [date];
        }
    }
}