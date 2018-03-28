using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRatingRepository _movieRatingRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public MovieService(IMovieRatingRepository movieRatingRepository, IMovieRepository movieRepository, IUserRepository userRepository)
        {
            _movieRatingRepository = movieRatingRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
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

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnTotalUserAverageRatingsAsync(int takeCount)
        {
            try
            {
                return await _movieRatingRepository.GetTopMoviesBasedOnTotalUserAverageRatingsAsync(takeCount);
            }
            catch (Exception)
            {
                // log the exception
            }
            return new List<MovieDto>();
        }

        public async Task<IReadOnlyList<MovieDto>> GetTopMoviesBasedOnHighestUserRatingAsync(int takeCount, int userId)
        {
            try
            {
                return await _movieRatingRepository.GetTopMoviesBasedOnHighestUserRatingAsync(takeCount, userId);
            }
            catch (Exception)
            {
                // log the exception
            }
            return new List<MovieDto>();
        }

        public async Task<bool> AddRatingAsync(int movieId, int userId, int rating)
        {
            try
            {
                bool movieExists = await _movieRepository.ExistsAsync(movieId);
                if (!movieExists)
                {
                    return false;
                }

                var userExists = await _userRepository.ExistsAsync(userId);
                if (!userExists)
                {
                    return false;
                }

                var movieUserRating = await _movieRatingRepository.GetByIdAsync(movieId, userId);
                if (movieUserRating == null)
                {
                    movieUserRating = new MovieUserRating(movieId, userId, rating);
                    await _movieRatingRepository.CreateAsync(movieUserRating);
                    return true;
                }

                movieUserRating.SetRating(rating);
                await _movieRatingRepository.UpdateAsync(movieUserRating);
            }
            catch (Exception)
            {
                // log the exception
            }

            return true;
        }
    }
}
