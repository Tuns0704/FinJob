﻿using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
