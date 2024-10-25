namespace PensionFund.API.Contracts.PersonContract
{
    public record PersonRequest(
        string FullName,
        int Age,
        string Gender,
        decimal Salary,
        string WorkExperience,
        string CityResidence
    );
}
