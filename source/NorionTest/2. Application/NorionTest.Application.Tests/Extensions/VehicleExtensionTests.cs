using FluentAssertions;
using NorionTest.Application.Extensions;
using NorionTest.Domain;
using NorionTest.Domain.Interfaces;
using NSubstitute;

namespace NorionTest.Application.Tests.Extensions;

public class VehicleExtensionTests
{
    [Fact]
    public void IsTollFreeVehicle_WithMotorbikeAsType_ShouldReturnTrue()
    {
        var vehicle = new Motorbike();

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }

    [Fact]
    public void IsTollFreeVehicle_WithEmergencyAsType_ShouldReturnTrue()
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(TollFreeVehicles.Emergency.ToString());

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsTollFreeVehicle_WithTractorAsType_ShouldReturnTrue()
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(TollFreeVehicles.Tractor.ToString());

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsTollFreeVehicle_WithDiplomatAsType_ShouldReturnTrue()
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(TollFreeVehicles.Diplomat.ToString());

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsTollFreeVehicle_WithForeignAsType_ShouldReturnTrue()
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(TollFreeVehicles.Foreign.ToString());

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }
        
    [Fact]
    public void IsTollFreeVehicle_WithMilitaryAsType_ShouldReturnTrue()
    {
        var vehicle = Substitute.For<IVehicle>();
        vehicle.GetVehicleType().Returns(TollFreeVehicles.Military.ToString());

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsTollFreeVehicle_WithCarAsType_ShouldReturnFalse()
    {
        var vehicle = new Car();

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeFalse();
    }
    
   

    [Fact]
    public void IsTollFreeVehicle_WithNull_ShouldReturnFalse()
    {
        IVehicle? vehicle = null;

        var result = vehicle.IsTollFreeVehicle();

        result.Should().BeFalse();
    }
}