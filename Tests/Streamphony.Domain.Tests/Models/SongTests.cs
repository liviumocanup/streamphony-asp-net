using System.ComponentModel.DataAnnotations;
using Streamphony.Domain.Models;

namespace Streamphony.Domain.Tests.Models
{
    public class SongTests
    {
        [Fact]
        public void Title_WithInvalidLength_ShouldFailValidation()
        {
            var song = new Song { Title = new string('a', 51) };

            var isValid = TryValidateModel(song, out var validationResults);

            Assert.Multiple(
                () => Assert.False(isValid),
                () => Assert.Contains(validationResults, v => v.ErrorMessage!.Contains("Song title must be between 1 and 50 characters"))
            );
        }

        [Fact]
        public void Url_WithInvalidUrl_ShouldFailValidation()
        {
            var song = new Song { Url = "invalid_url" };

            var isValid = TryValidateModel(song, out var validationResults);

            Assert.Multiple(
                () => Assert.False(isValid),
                () => Assert.Contains(validationResults, v => v.ErrorMessage!.Contains("The URL must be a valid URL."))
            );
            
        }

        [Fact]
        public void ValidSong_WithAllRequiredFields_ShouldPassValidation()
        {
            var song = new Song
            {
                Title = "Valid Title",
                Duration = new TimeSpan(0, 3, 15),
                Url = "http://example.com/song.mp3"
            };
            var isValid = TryValidateModel(song, out _);

            Assert.True(isValid);
        }

        private static bool TryValidateModel(object model, out List<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, context, results, true);
        }
    }
}