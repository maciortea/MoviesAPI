using System;
using System.Runtime.Serialization;

namespace ApplicationCore.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        [IgnoreDataMember]
        public double AverageRatingInternal { get; set; }
        public double AverageRating
        {
            get { return Math.Round(AverageRatingInternal * 2D) * 0.5D; }
        }
    }
}
