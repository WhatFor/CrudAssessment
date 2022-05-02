using System.Collections.Generic;
using System.Linq;
using Crud.Shared.Models;
using FluentAssertions;
using Xunit;

namespace Crud.UnitTests.Infrastructure;

public class ResultTests
{
    [Fact]
    public void UnitResultSuccessful_ShouldReturnSuccessEqualsTrue()
    {
        var result = UnitResult.Successful();
        
        result.Success.Should().BeTrue();
    }
    
    [Fact]
    public void UnitResultWithSingleError_ShouldReturnErrors()
    {
        var errorMessage = "Error!";
        
        var result = UnitResult.Error(errorMessage);

        result.Success.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().Be(errorMessage);
    }
    
    [Fact]
    public void UnitResultWithMultipleErrors_ShouldReturnErrors()
    {
        var errorMessages = new List<string> { "Error!", "Error 2!" };
        
        var result = UnitResult.Error(errorMessages);

        result.Success.Should().BeFalse();
        result.Errors.Should().HaveCount(errorMessages.Count);
        result.Errors.Should().Equal(errorMessages);
    }
    
    [Fact]
    public void ResultSuccessful_ShouldReturnSuccessEqualsTrueAndContainData()
    {
        var result = Result<bool>.Successful(true);
        
        result.Success.Should().BeTrue();
        result.Data.Should().Be(true);
    }
    
    [Fact]
    public void ResultWithSingleError_ShouldReturnErrors()
    {
        var errorMessage = "Error!";
        
        var result = Result<bool>.Error(errorMessage);

        result.Success.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().Be(errorMessage);
    }
    
    [Fact]
    public void ResultWithMultipleErrors_ShouldReturnErrors()
    {
        var errorMessages = new List<string> { "Error!", "Error 2!" };
        
        var result = Result<bool>.Error(errorMessages);

        result.Success.Should().BeFalse();
        result.Errors.Should().HaveCount(errorMessages.Count);
        result.Errors.Should().Equal(errorMessages);
    }
}