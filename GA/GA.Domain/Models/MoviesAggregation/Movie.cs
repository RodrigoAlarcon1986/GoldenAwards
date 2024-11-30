namespace GA.Domain.Models.MoviesAggregation
{
    public class Movie : Entity<Guid>, IAggregateRoot
    {
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Studios { get; set; } = string.Empty;
        public string Producers { get; set; } = string.Empty;
        public bool? IsWinner { get; set; }
    }
}
