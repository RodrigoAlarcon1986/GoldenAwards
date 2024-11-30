namespace GA.Domain.Dtos.Movies
{
    public record WinnerDto(string Producer, int Interval, int PreviousWin, int FollowingWin);
}
