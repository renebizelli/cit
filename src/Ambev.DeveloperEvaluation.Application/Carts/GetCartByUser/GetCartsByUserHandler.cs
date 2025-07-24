using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUser;

internal class GetCartsByUserHandler : IRequestHandler<GetCartByUserCommand>
{
    private readonly ICacheRepository _cache;

    public GetCartsByUserHandler(ICacheRepository cache)
    {
        _cache = cache;
    }

    public async Task Handle(GetCartByUserCommand request, CancellationToken cancellationToken)
    {
        await _cache.SetAsync("test", request);

        var x = await _cache.GetAsync<GetCartByUserCommand>("test");
    }
}
