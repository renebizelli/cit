using Ambev.DeveloperEvaluation.Application.Categories._Services;
using Ambev.DeveloperEvaluation.Application.Categories._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

public class CategoryServiceTests
{
    private readonly ICategoryRepository _repository = Substitute.For<ICategoryRepository>();
    private readonly ICacheRepository _cache = Substitute.For<ICacheRepository>();
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _service = new CategoryService(_repository, _cache);
    }

    [Fact]
    public async Task GetAsync_ReturnsCategory_FromCache()
    {
        var category = new Category { Id = 1, Name = "Drinks" };
        _cache.GetAsync<Category>(Arg.Any<CategoryCacheGetOptions>()).Returns(category);

        var result = await _service.GetAsync(1, CancellationToken.None);

        result.Should().Be(category);
        await _repository.DidNotReceive().GetAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetAsync_ReturnsCategory_FromRepository_AndSetsCache()
    {
        var category = new Category { Id = 2, Name = "Food" };
        _cache.GetAsync<Category>(Arg.Any<CategoryCacheGetOptions>()).ReturnsNull();
        _repository.GetAsync(2, Arg.Any<CancellationToken>()).Returns(category);

        var result = await _service.GetAsync(2, CancellationToken.None);

        result.Should().Be(category);
        await _cache.Received(1).SetAsync(Arg.Any<CategoryCacheSetOptions>(), category);
    }

    [Fact]
    public async Task GetAsync_ReturnsNull_WhenCategoryNotFound()
    {
        _cache.GetAsync<Category>(Arg.Any<CategoryCacheGetOptions>()).ReturnsNull();
        _repository.GetAsync(3, Arg.Any<CancellationToken>()).ReturnsNull();

        var result = await _service.GetAsync(3, CancellationToken.None);

        result.Should().BeNull();
    }
}