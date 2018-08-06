using System;
using System.Linq;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method)]
    public class ValidateReferenceAttribute : ValidatableAttribute
    {
        public string ValuePath { get; set; }

        public ValidateReferenceAttribute(string valuePath)
        {
            ValuePath = valuePath;
        }

        private bool IsValid(IMemberInfo memberInfo, string path)
        {
            if (memberInfo == null)
                throw new Exception($"{path} is null");

            var split = ValuePath.Split('.');
            if (split.Length < 2)
                throw new Exception("Property path invalid");

            var obj = typeof(ValidateReferenceAttribute).Assembly
                .GetTypes()
                .FirstOrDefault(o => o.Name.Equals(split[0], StringComparison.CurrentCultureIgnoreCase));

            if (obj == null)
                throw new Exception($"Type ({split[0]}) was not found. Called from {path}");

            var obj2 = new TypeInfo(obj);

            var resolvedProperty = obj2.Properties
                .FirstOrDefault(o => o.Name.Equals(split[1], StringComparison.CurrentCultureIgnoreCase));

            if (resolvedProperty == null)
                throw new Exception($"Property not resolved: {ValuePath} on type {obj.Name}.");
            
            var type = memberInfo.Type;

            var propertyType = resolvedProperty.Type;

            var isSame = type == propertyType;

            if (!isSame)
                throw new Exception($"{path} type or return type is does not match the expected type at {ValuePath}");

            return isSame;
        }

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<IPropertyInfo>(info, propertyInfo =>
            {

            });

            return true;
        }
    }
}