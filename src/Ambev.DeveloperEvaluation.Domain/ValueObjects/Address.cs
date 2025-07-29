﻿namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Address
{
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string GeoLocation { get; set; } = string.Empty;
}