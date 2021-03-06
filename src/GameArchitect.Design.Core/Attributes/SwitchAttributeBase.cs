﻿using System;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    public enum SwitchType
    {
        Inclusive,
        Exclusive
    }

    public enum BooleanOperation
    {
        Add,
        Subtract
    }

    [AttributeUsage(AttributeTargets.All)]
    public abstract class SwitchAttributeBase : ValidatableAttribute
    {
        protected virtual SwitchType Type { get; } = SwitchType.Inclusive;
        protected virtual BooleanOperation Operation { get; } = BooleanOperation.Add;
        protected virtual string Key { get; } = string.Empty; // Case insensitive

        // TODO: Validate
        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            // TODO

            return true;
        }
    }
}