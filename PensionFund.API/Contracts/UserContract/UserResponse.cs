using System;

namespace PensionFund.API.Contracts.UserContract
{
    public record UserResponse(
        Guid Id,
        string Login,
        string Role,
        string Password
    );
}
