using Ambev.DeveloperEvaluation.Domain.Services;
using Coravel.Invocable;

namespace Ambev.DeveloperEvaluation.Jobs;

public class RecalculateProductRatingsJob : IInvocable
{
    private readonly IProductRatingService _productRatingService;

    public RecalculateProductRatingsJob(IProductRatingService productRatingService)
    {
        _productRatingService = productRatingService;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Recalculating product ratings...");

        await _productRatingService.RecalculateAsync(CancellationToken.None);
    }
}