namespace ApplicationCore.Entities
{
    public class MovieUserRating
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
    }
}
