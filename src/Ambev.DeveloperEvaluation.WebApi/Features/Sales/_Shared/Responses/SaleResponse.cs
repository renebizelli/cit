using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales._Shared.Responses;

public class SaleResponse
{
    public string Id { get; set; } = string.Empty;
    public long SaleNumber { get; set; }
    public BranchResponse Branch { get; set; } = new();
    public SaleStatus Status { get; set; }
    public UserResponse User { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }

    public class BranchResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SaleItemResponse
    {
        public string Id { get; set; } = string.Empty;
        public ProductResponse Product { get; set; } = new();
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }

        public class ProductResponse
        {
            public string Id { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }

    }


}
