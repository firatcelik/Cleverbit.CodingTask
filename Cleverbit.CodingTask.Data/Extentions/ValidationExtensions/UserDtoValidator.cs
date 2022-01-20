using Cleverbit.CodingTask.Data.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cleverbit.CodingTask.Data.Extentions.ValidationExtensions
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(p => p.UserName).MaximumLength(250).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(p => p.Password).MaximumLength(250).WithMessage("{PropertyName} must be less than {MaxLength} characters.");
        }
    }
}
