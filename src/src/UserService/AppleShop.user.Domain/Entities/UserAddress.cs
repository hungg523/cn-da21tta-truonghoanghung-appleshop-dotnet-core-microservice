﻿using AppleShop.user.Domain.Abstractions.Common;

namespace AppleShop.user.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
    }
}