using System.Threading.Tasks;
using Crud.Application.Contacts.Queries;

namespace Crud.IntegrationTests.Contacts.Queries;

public class GetPaginatedContactsQueryTests : TestBase
{
    [Fact]
    public async Task ShouldRequirePageNumber()
    {
        var query = new GetPaginatedContactsQuery { Count = 0 };
        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<FluentValidation.ValidationException>();
    }
    
    [Fact]
    public async Task ShouldRequireCount()
    {
        var query = new GetPaginatedContactsQuery { PageNumber = 0 };
        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<FluentValidation.ValidationException>();
    }
    
    [Fact]
    public async Task GetPaginatedContactsQuery_Returns_Paginated_Contacts()
    {
        // Arrange
        var query = new GetPaginatedContactsQuery
        {
            Count = 10,
            PageNumber = 1,
        };

        // Act
        var result = await SendAsync(query);

        // Assert : TODO
        result.Should().NotBeNull();
    }
}