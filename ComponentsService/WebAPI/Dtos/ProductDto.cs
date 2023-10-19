﻿using ComputerStore.Services.CS.Shared.Enums;

namespace WebAPI.Dtos;

public sealed class ProductDto
{
    public Guid? Id { get; init; }
    public string Title { get; init; } = default!;
    public decimal Price { get; init; }
    public string Description { get; init; } = default!;
    public ProductType Type { get; init; }
    public DateTime? CreatedDate { get; init; }
    public DateTime? ModifiedDate { get; init; }
}
