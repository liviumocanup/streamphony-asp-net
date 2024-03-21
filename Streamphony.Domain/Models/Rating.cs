using System;

namespace Streamphony.Domain.Models
{
    public class Rating : BaseEntity
    {
        private int _score;

        public int Score
        {
            get => _score;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException("Score must be between 1 and 5.");
                }
                _score = value;
            }
        }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}
