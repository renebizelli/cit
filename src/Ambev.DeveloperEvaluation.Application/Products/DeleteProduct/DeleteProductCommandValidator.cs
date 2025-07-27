﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("##TODO: Product ID must not be empty.");
    }
}
