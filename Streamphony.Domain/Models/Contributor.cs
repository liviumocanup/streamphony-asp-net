using System;
using System.Collections.Generic;

namespace Streamphony.Domain.Models
{
    public class Contributor : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; }
    }
}