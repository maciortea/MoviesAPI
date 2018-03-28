using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class MovieServiceTest
    {
        private readonly Mock<IMovieRatingRepository> _movieRatingRepositoryMock;
        private readonly Mock<IMovieRepository> _movieRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMovieService _movieService;

        public MovieServiceTest()
        {
            _movieRatingRepositoryMock = new Mock<IMovieRatingRepository>();
            _movieRepositoryMock = new Mock<IMovieRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _movieService = new MovieService(_movieRatingRepositoryMock.Object, _movieRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public void QueryMoviesAsync_ShouldReturnList()
        {
            // Arrange
            IReadOnlyList<MovieDto> list = new List<MovieDto>();
            _movieRatingRepositoryMock.Setup(x => x.QueryMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(list));

            // Act
            var result = _movieService.QueryMoviesAsync("title", "genre", 1999).Result;

            // Assert
            Assert.True(result.Count <= 0);
        }

        [Fact]
        public void GetTopMoviesBasedOnTotalUserAverageRatingsAsync_ShouldReturnList()
        {
            // Arrange
            IReadOnlyList<MovieDto> list = new List<MovieDto>();
            _movieRatingRepositoryMock.Setup(x => x.GetTopMoviesBasedOnTotalUserAverageRatingsAsync(It.IsAny<int>())).Returns(Task.FromResult(list));

            // Act
            var result = _movieService.GetTopMoviesBasedOnTotalUserAverageRatingsAsync(5).Result;

            // Assert
            Assert.True(result.Count <= 0);
        }

        [Fact]
        public void GetTopMoviesBasedOnHighestUserRatingAsync_ShouldReturnList()
        {
            // Arrange
            IReadOnlyList<MovieDto> list = new List<MovieDto>();
            _movieRatingRepositoryMock.Setup(x => x.GetTopMoviesBasedOnHighestUserRatingAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(list));

            // Act
            var result = _movieService.GetTopMoviesBasedOnHighestUserRatingAsync(5, 1).Result;

            // Assert
            Assert.True(result.Count <= 0);
        }

        [Fact]
        public void AddRatingAsync_MovieDoesntExists_ShouldReturnFalse()
        {
            // Arrange
            _movieRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            var result = _movieService.AddRatingAsync(1, 2, 3).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddRatingAsync_UserDoesntExists_ShouldReturnFalse()
        {
            // Arrange
            _movieRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            // Act
            var result = _movieService.AddRatingAsync(1, 2, 3).Result;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddRatingAsync_ExistingMovieRating_ShouldUpdateAndReturnTrue()
        {
            // Arrange
            _movieRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(true));

            var movieUserRating = new MovieUserRating(1, 1, 1);
            _movieRatingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(movieUserRating));
            _movieRatingRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<MovieUserRating>())).Returns(Task.CompletedTask);

            // Act
            var result = _movieService.AddRatingAsync(1, 1, 1).Result;

            // Assert
            _movieRatingRepositoryMock.Verify(x => x.UpdateAsync(movieUserRating), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void AddRatingAsync_NewMovieRating_ShouldCreateAndReturnTrue()
        {
            // Arrange
            _movieRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            _movieRatingRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult<MovieUserRating>(null));
            _movieRatingRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<MovieUserRating>())).Returns(Task.CompletedTask);

            // Act
            var result = _movieService.AddRatingAsync(1, 1, 1).Result;

            // Assert
            _movieRatingRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<MovieUserRating>()), Times.Once);
            Assert.True(result);
        }
    }
}
