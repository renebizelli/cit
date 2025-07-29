namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

public interface ISalesQuerySettings : IUserBranchKey, IListSettings
{
    string City { get; set; }
    string State { get; set; }
    DateTime MinDate { get; set; }
    DateTime MaxDate { get; set; }
    decimal MinTotalAmount { get; set; }
    decimal MaxTotalAmount { get; set; }
}
