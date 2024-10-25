namespace PensionFund.API.Contracts.UserContract
{
    public record UserRequest(
        string Login,
        string Password,
        string Role
    );
}
