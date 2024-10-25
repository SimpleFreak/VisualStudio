namespace PensionFund.Application.Interfaces.Auth;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string hashedPassword, string providedPassword);
}