namespace NorionTest.Application.Toll.TollCalculators.Models;

public record TollFeePeriod(TimeOnly StartTime, TimeOnly EndTime, int Fee)
{
    public bool IsWithinPeriod(DateTime date)
    {
        var time = TimeOnly.FromDateTime(date); 
        return time <= EndTime && time >= StartTime;
    }
}