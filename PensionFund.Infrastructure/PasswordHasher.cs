﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PensionFund.Application.Interfaces.Auth;

namespace PensionFund.Infrastructure;

/* Класс для хэширования паролей */
public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        return Convert.ToBase64String(salt) + ":" + hashed;
    }

    public bool Verify(string hashedPassword, string providedPassword)
    {
        var parts = providedPassword.Split(':');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];
        var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: hashedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));

        return hash == computedHash;
    }
}