using Ambev.DeveloperEvaluation.Application._Shared;
using Ambev.DeveloperEvaluation.Application.Sales._Shared.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : UserBranchKey, IRequest<SaleResult>
{
}
