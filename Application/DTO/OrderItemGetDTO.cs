﻿namespace Application.DTO
{
    public class OrderItemGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public string Type { get; set; } = null!;
        public int FoodId { get; set; }
    }
}
