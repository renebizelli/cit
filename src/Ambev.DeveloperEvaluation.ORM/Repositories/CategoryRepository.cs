using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DefaultContext _context;

    public CategoryRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Categories.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}
