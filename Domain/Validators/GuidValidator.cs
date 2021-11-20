using System;
using System.Collections.Generic;
using FluentValidation;

namespace Domain.Validators
{
    public static class GuidValidator
    {
        public static IRuleBuilderOptions<T, string> MustBeAGuid<T>(this IRuleBuilder<T, string> ruleBuilder) {
            return ruleBuilder.Must(guid => Guid.TryParse(guid, out _))
                .WithMessage("ListId must be a valid Guid");
        }
    }
}