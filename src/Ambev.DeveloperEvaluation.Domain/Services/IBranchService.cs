﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IBranchService
{
    Task<Branch?> GetAsync(Guid id, CancellationToken cancellationToken);
}

