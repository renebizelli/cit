﻿using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product  
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Category? Category { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public bool Active { get; set; }
    public RatingValues Rating { get; set; } = new();

    public void Activate()
    {
        Active = true;
    }

    public class RatingValues
    {
        public double Avg { get; set; }
        public long Count { get; set; }
    }
}

