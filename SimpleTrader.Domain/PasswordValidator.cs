using FluentValidation;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x)
                .MinimumLength(6)
                .WithMessage("Minimum length is 6")
                .Matches("[a-z]")
                .WithMessage("You need to have at least one lowercase character")
                .Matches("[A-Z]")
                .WithMessage("You need to have at least one uppercase character")
                .Matches("[0-9]")
                .WithMessage("You need to have at least one digit")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("You need to have at least one special character");
        }
    }
}
