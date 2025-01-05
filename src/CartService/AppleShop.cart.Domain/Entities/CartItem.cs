﻿using AppleShop.cart.Domain.Abstractions.Common;

namespace AppleShop.cart.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Cart Cart { get; set; }
    }
}