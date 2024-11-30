namespace GA.Domain.Dtos.Movies
{
    public record MinMaxWinnersDto (IEnumerable<WinnerDto> Min, IEnumerable<WinnerDto> Max);
}
