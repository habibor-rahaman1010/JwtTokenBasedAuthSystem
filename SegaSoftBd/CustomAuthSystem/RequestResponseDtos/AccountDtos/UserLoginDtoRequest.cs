﻿using System.ComponentModel.DataAnnotations;

namespace CustomAuthSystem.RequestResponseDtos.AccountDtos
{
    public class UserLoginDtoRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
