﻿using System.ComponentModel.DataAnnotations;

namespace CandidateDetailsMVC.Models
{
    public class CandidateDetails
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
    }
}
