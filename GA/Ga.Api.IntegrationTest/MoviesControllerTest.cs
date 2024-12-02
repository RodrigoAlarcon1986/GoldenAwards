using FluentAssertions;
using GA.Domain.Dtos.Movies;
using System.Net.Http.Json;

namespace Ga.Api.IntegrationTest
{
    public class MoviesControllerTest
    {
        [Theory]
        [InlineData("../../../CsvDataTest/movieslist.csv", 1, 1, 1, 13)]
        [InlineData("../../../CsvDataTest/movieslist_1.csv", 2, 1, 1, 18)]
        [InlineData("../../../CsvDataTest/movieslist_2.csv", 2, 2, 5, 29)]
        [InlineData("../../../CsvDataTest/movieslist_3.csv", 1, 1, 18, 18)]
        public async Task GetMinMaxWinners_WhenCsfFileIsNotEmpty_ReturnsMinMaxWinners(string pathToCsv, int minTotalCount, int maxTotalCount, int minInterval, int maxInterval)
        {
            // Arrange
            var factory = new GaWebApplicationFactory(pathToCsv);
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/movies/getminmaxwinners");

            // Assert
            response.EnsureSuccessStatusCode();
            var minMaxWinnersDto = await response.Content.ReadFromJsonAsync<MinMaxWinnersDto>();

            minMaxWinnersDto.Should().NotBeNull();
            minMaxWinnersDto!.Min.Should().HaveCount(minTotalCount);
            minMaxWinnersDto.Min.ElementAt(0).Interval.Should().Be(minInterval);
            minMaxWinnersDto.Max.Should().HaveCount(maxTotalCount);
            minMaxWinnersDto.Max.ElementAt(0).Interval.Should().Be(maxInterval);
        }
    }
}
