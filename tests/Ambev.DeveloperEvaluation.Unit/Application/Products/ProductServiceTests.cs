using Ambev.DeveloperEvaluation.Application.Products._Services;
using Ambev.DeveloperEvaluation.Application.Products._Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

public class ProductServiceTests
{
    private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
    private readonly ICategoryService _categoryService = Substitute.For<ICategoryService>();
    private readonly ICacheRepository _cache = Substitute.For<ICacheRepository>();
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _service = new ProductService(_repository, _categoryService, _cache);
    }

    private static Product CreateProduct(string id = "prod1", string title = "Beer", int categoryId = 1)
    {
        return new Product
        {
            Id = id,
            Title = title,
            Description = "desc",
            Category = new Category { Id = categoryId, Name = "Drinks" },
            Price = 10m,
            Image = "img.png",
            Active = false
        };
    }

    [Fact]
    public async Task CreateOrUpdateAsync_Throws_WhenNameAlreadyUsed()
    {
        var product = CreateProduct();
        _repository.IsNameAlreadyUsedAsync(product.Title.Trim(), Arg.Any<CancellationToken>()).Returns(true);

        var act = async () => await _service.CreateOrUpdateAsync(product, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CreateOrUpdateAsync_Throws_WhenCategoryIsNull()
    {
        var product = CreateProduct();
        product.Category = null;
        _repository.IsNameAlreadyUsedAsync(product.Title.Trim(), Arg.Any<CancellationToken>()).Returns(false);

        var act = async () => await _service.CreateOrUpdateAsync(product, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Invalid Category*");
    }

    [Fact]
    public async Task CreateOrUpdateAsync_Throws_WhenCategoryIdIsZero()
    {
        var product = CreateProduct();
        product.Category.Id = 0;
        _repository.IsNameAlreadyUsedAsync(product.Title.Trim(), Arg.Any<CancellationToken>()).Returns(false);

        var act = async () => await _service.CreateOrUpdateAsync(product, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Invalid Category*");
    }

    [Fact]
    public async Task CreateOrUpdateAsync_EnrichesCategory_AndActivatesProduct()
    {
        var product = CreateProduct();
        var enrichedCategory = new Category { Id = product.Category.Id, Name = "Enriched" };
        _repository.IsNameAlreadyUsedAsync(product.Title.Trim(), Arg.Any<CancellationToken>()).Returns(false);
        _categoryService.GetAsync(product.Category.Id, Arg.Any<CancellationToken>()).Returns(enrichedCategory);
        _repository.CreateOrUpdateAsync(product, Arg.Any<CancellationToken>()).Returns(product);

        var result = await _service.CreateOrUpdateAsync(product, CancellationToken.None);

        result.Should().Be(product);
        product.Category.Should().Be(enrichedCategory);
        product.Active.Should().BeTrue();
    }

    [Fact]
    public async Task ListProductsAsync_ReturnsProducts()
    {
        var querySettings = Substitute.For<IProductQuerySettings>();
        var products = new List<Product> { CreateProduct(), CreateProduct("prod2", "Wine", 2) };
        _repository.ListProductsAsync(querySettings, Arg.Any<Dictionary<string, System.Linq.Expressions.Expression<Func<Product, object>>>>(), Arg.Any<CancellationToken>())
            .Returns((products.Count, products));

        var (count, result) = await _service.ListProductsAsync(querySettings, CancellationToken.None);

        count.Should().Be(products.Count);
        result.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task GetAsync_Throws_WhenProductIdIsNullOrEmpty()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetAsync(null!, CancellationToken.None));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetAsync("", CancellationToken.None));
    }

    [Fact]
    public async Task GetAsync_ReturnsProduct_FromCache()
    {
        var product = CreateProduct();
        _cache.GetAsync<Product>(Arg.Any<ProductCacheGetOptions>()).Returns(product);

        var result = await _service.GetAsync(product.Id, CancellationToken.None);

        result.Should().Be(product);
        await _repository.DidNotReceive().GetAsync(product.Id, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetAsync_ReturnsProduct_FromRepository_AndSetsCache()
    {
        var product = CreateProduct();
        _cache.GetAsync<Product>(Arg.Any<ProductCacheGetOptions>()).ReturnsNull();
        _repository.GetAsync(product.Id, Arg.Any<CancellationToken>()).Returns(product);

        var result = await _service.GetAsync(product.Id, CancellationToken.None);

        result.Should().Be(product);
        await _cache.Received(1).SetAsync(Arg.Any<ProductCacheSetOptions>(), product);
    }

    [Fact]
    public async Task GetAsync_ReturnsNull_WhenProductNotFound()
    {
        _cache.GetAsync<Product>(Arg.Any<ProductCacheGetOptions>()).ReturnsNull();
        _repository.GetAsync("notfound", Arg.Any<CancellationToken>()).ReturnsNull();

        var result = await _service.GetAsync("notfound", CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenProductIdIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.DeleteAsync(null!, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenProductIdIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteAsync("", CancellationToken.None));
    }

    [Fact]
    public async Task DeleteAsync_DeletesFromCacheAndRepository()
    {
        var productId = "prod1";
        _repository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(1);

        await _service.DeleteAsync(productId, CancellationToken.None);

        await _cache.Received(1).DeleteAsync(Arg.Any<ProductCacheDeleteOptions>());
        await _repository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ThrowsKeyNotFoundException_WhenRepositoryReturnsZero()
    {
        var productId = "prod1";
        _repository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(0);

        var act = async () => await _service.DeleteAsync(productId, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("*Product not found*");
    }
}