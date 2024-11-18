﻿using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.productCategory.commandApplication.Commands.ProductImage
{
    public class UpdateProductImageCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? ImageData { get; set; }
        public int? Position { get; set; } = 0;
        public int? ProductId { get; set; }
    }
}