﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionFund.Core.Models
{
    public class User
    {
        [Key, Required, NotNull]
        public Guid Id { get; set; }

        [Required, NotNull]
        public string Login { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), NotNull]
        public string Password { get; set; } = string.Empty;

        [Required, NotNull]
        public string Role { get; set; } = string.Empty;

        private User(Guid id, string login, string password, string role)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
        }

        public static (User User, string Error) Create(Guid id, string login,
            string password, string role)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(login))
            {
                error = "Login cannot be undefined or empty.";

            }

            if (string.IsNullOrEmpty(password))
            {
                error += "\nPassword cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(role))
            {
                error += "\nRole cannot be undefined or empty.";
            }

            var user = new User(id, login, password, role);

            return (user, error);
        }
    }
}
