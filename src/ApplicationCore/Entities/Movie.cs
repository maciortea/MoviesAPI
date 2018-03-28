using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTimeInMinutes { get; set; }

        public IReadOnlyList<MovieGenre> MovieGenres { get; set; }

        public Movie()
        {
            MovieGenres = new List<MovieGenre>();
        }
    }
}
