namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface ISalesQuerySettings : IUserBranchKey, IListSettings
{
    DateTime MinDate { get; set; } 
    DateTime MaxDate { get; set; }
    decimal MinTotalPrice { get; set; }
    decimal MaxTotalPrice { get; set; }
}
