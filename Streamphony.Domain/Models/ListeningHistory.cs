using System;
using System.ComponentModel.DataAnnotations;

namespace Streamphony.Domain.Models
{
    public class ListeningHistory : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid SongId { get; set; }
        public Song? Song { get; set; }
        public long ListenCount { get; set; } = 0;
        public DateTime LastPlayedAt { get; set; }

        public void RecordPlay()
        {
            ListenCount++;
            LastPlayedAt = DateTime.UtcNow;
        }
    }
}
