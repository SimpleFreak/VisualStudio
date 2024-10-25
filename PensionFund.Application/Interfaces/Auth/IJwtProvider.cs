using PensionFund.Core.Models;

namespace PensionFund.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}