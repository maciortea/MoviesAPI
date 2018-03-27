using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRatingRepository _movieRatingRepository;

        public MovieService(IMovieRatingRepository movieRatingRepository)
        {
            _movieRatingRepository = movieRatingRepository;
        }

        public async Task<IReadOnlyList<MovieDto>> QueryMoviesAsync(string title, string genre, int? yearOfRelease = null)
        {
            try
            {
                return await _movieRatingRepository.QueryMoviesAsync(title, genre, yearOfRelease);
            }
            catch (Exception)
            {
                // log the exception
            }
            return new List<MovieDto>();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatings(int takeCount)
        {
            try
            {
                return await _movieRatingRepository.GetTopMoviesBasedOnTotalUserAverageRatings(takeCount);
            }
            catch (Exception)
            {
                // log the exception
            }
            return new List<MovieDto>();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRating(int takeCount, int userId)
        {
            try
            {
                return await _movieRatingRepository.GetTopMoviesBasedOnHighestUserRating(takeCount, userId);
            }
            catch (Exception)
            {
                // log the exception
            }
            return new List<MovieDto>();
        }
    }
}
