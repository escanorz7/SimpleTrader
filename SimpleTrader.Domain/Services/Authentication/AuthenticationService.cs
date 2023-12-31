﻿using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountService accountService, IPasswordHasher passwordHasher)
        {
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> Login(string userName, string password)
        {
            Account storedAccount = await _accountService.GetByUserName(userName);

            if (storedAccount == null)
            {
                throw new UserNotFoundException();
            }

            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(storedAccount.AccountHolder.Password, password);
            if (passwordResult != PasswordVerificationResult.Success)
            {
                throw new InvalidPasswordException();
            }

            return storedAccount;
        }

        public async Task<RegistrationResult> Register(string email, string userName, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            PasswordValidator validator = new PasswordValidator();

            ValidationResult results = validator.Validate(password);

            if (!results.IsValid)
            {
                result = RegistrationResult.WeakPassword;
            }

            else if (password != confirmPassword)
            {
                result = RegistrationResult.PasswordsDoNotMatch;
            }

            Account emailAccount = await _accountService.GetByEmail(email);

            if (emailAccount != null)
            {
                result = RegistrationResult.EmailAlreadyExists;
            }

            Account userNameAccount = await _accountService.GetByUserName(userName);

            if (userNameAccount != null)
            {
                result = RegistrationResult.UserNameAlreadyExists;
            }

            if (result == RegistrationResult.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);

                User user = new User()
                {
                    Email = email,
                    UserName = userName,
                    Password = hashedPassword,
                    DateJoined = DateTime.UtcNow
                };

                Account account = new Account()
                {
                    AccountHolder = user,
                    Balance = 1000
                };

                await _accountService.Create(account);
            }

            return result;
        }
    }
}
