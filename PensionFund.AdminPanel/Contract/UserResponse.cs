using System;

namespace PensionFund.AdminPanel.Contract
{
    public record UserResponse(
        Guid Id,
        string Login,
        string Role,
        string Password = ""
    );
}
