using System;

namespace PensionFund.AdminPanel.Contract
{
    public record UserRequest(
        Guid Id,
        string Login,
        string Role,
        string Password = ""
    );
}
