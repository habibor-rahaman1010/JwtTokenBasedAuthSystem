﻿namespace CustomAuthSystem.RequestResponseDtos.AccountDtos
{
    public class RefreshTokenRequestDto
    {
        public Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
