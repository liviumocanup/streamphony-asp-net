using System.ComponentModel.DataAnnotations;
using Streamphony.Domain.Models;

namespace Streamphony.Domain.Tests.Models.Auth
{
    public class UserTests
    {
        [Fact]
        public void ArtistName_WhenTooLong_ShouldFailValidation()
        {
            var user = new User { ArtistName = new string('a', 101) };

            var isValid = TryValidateModel(user, out var validationResults);

            Assert.Multiple(
                () => Assert.False(isValid),
                () => Assert.Contains(validationResults, v => v.ErrorMessage!.Contains("Artist name must be between 1 and 100 characters"))
            );
        }

        [Fact]
        public void DateOfBirth_WhenSetToFutureDate_ThrowsArgumentException()
        {
            var user = new User();

            Assert.Throws<ArgumentException>(() => user.DateOfBirth = DateTime.UtcNow.AddDays(1));
        }

        [Fact]
        public void ValidUser_WithAllRequiredFields_ShouldPassValidation()
        {
            var user = new User
            {
                ArtistName = "Valid Artist",
                DateOfBirth = DateTime.UtcNow.AddYears(-20),
                ProfilePictureUrl = "http://example.com/picture.jpg"
            };

            var isValid = TryValidateModel(user, out _);

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