using System;

namespace ApplicationCore.Entities
{
    public class MovieUserRating
    {
        public int MovieId { get; protected set; }

        public Movie Movie { get; protected set; }

        public int UserId { get; protected set; }

        public int Rating { get; protected set; }

        private MovieUserRating()
        {
        }

        public MovieUserRating(int movieId, int userId, int rating)
        {
            if (movieId <= 0)
            {
                throw new ArgumentException(nameof(movieId));
            }

            if (userId <= 0)
            {
                throw new ArgumentException(nameof(userId));
            }

            SetRating(rating);

            MovieId = movieId;
            UserId = userId;
        }

        public void SetRating(int rating)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentException(nameof(rating));
            }

            Rating = rating;
        }
    }
}
