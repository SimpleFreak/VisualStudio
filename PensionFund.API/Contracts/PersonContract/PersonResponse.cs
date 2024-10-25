using System;

namespace PensionFund.API.Contracts.PersonContract
{
    public record PersonResponse(
        Guid Id,
        string FullName,
        int Age,
        string Gender,
        decimal Salary
    );
}
