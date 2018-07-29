using System;
using System.Collections.Generic;
using System.Linq;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class DeconstructAttribute : ValidatableAttribute
    {
        public DeconstructCondition Condition { get; set; } = DeconstructCondition.WhenTypeUnsupported;
        public List<string> Properties { get; private set; }

        public DeconstructAttribute() { }

        public DeconstructAttribute(params string[] properties)
        {
            Properties = properties.ToList();
        }

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<IMemberInfo>(info, o =>
            {
                if(o.Type == typeof(void))
                    logger.LogError($"A deconstruct attribute was specified for {o.GetPath()} with a void return type.");

                var type = o.Type;
                if (Properties == null || Properties.Count == 0)
                {
                    if (type.ImplementsInterface<IDeconstructible>())
                        Properties = type.Create<IDeconstructible>().Deconstruct().ToList();
                    else
                    {
                        logger.LogError($"A Deconstruct attribute is specified for {o.GetPath()} but no Properties were provided, and the type {type.GetPath()} does not implement IDeconstructible.");
                        return;
                    }
                }

                var typeProperties = type.GetProperties()
                    .Select(p => p.Name)
                    .ToList();

                if (!Properties.All(p => typeProperties.Contains(p)))
                    logger.LogError($"One or more properties specified for a deconstructible (on {o.GetPath()}) were not found on the actual type ({o.Type.GetPath()}). Note that the names are case sensitive.");
            });

            return true;
        }

        private static void DeconstructPath(ILogger<IValidatable> logger, TypeInfo type, string path, ref List<IMemberInfo> result)
        {
            if (path.Length == 0)
                return;

            var actualProperties = type.GetProperties();
            var propertyName = path.Split('.')[0];

            var actualProperty = actualProperties.FirstOrDefault(o => o.Name == propertyName);
            if (actualProperty == null)
                logger.LogError($"Tried to deconstruct with invalid property name ({propertyName}) on type {type.GetPath()}.");

            // TODO
            //result.Add(new MemberInfo(actualProperty.Type, propertyName));

            if (!path.Contains('.'))
                return;

            path = path.Substring(path.IndexOf('.') + 1);
            DeconstructPath(logger, actualProperty.Type, path, ref result);
        }

        private static void Deconstruct(ILogger<IValidatable> logger, TypeInfo type, string name, List<string> deconstructionProperties, ref List<IMemberInfo> result)
        {
            var propertyPaths = deconstructionProperties;
            if (propertyPaths == null || propertyPaths.Count == 0)
            {
                if(!type.ImplementsInterface<IDeconstructible>())
                    throw new Exception($"Type/Name pair with Deconstruct attribute expected type ({type.Name} {name}) to implement IDeconstructible when PropertyNames are empty.");

                propertyPaths = type.Create<IDeconstructible>().Deconstruct().ToList();
            }

            foreach (var propertyPath in propertyPaths)
            {
                var deconstructedPath = new List<IMemberInfo>();
                DeconstructPath(logger, type, propertyPath, ref deconstructedPath);

                // TODO
                //result.Add(new MemberInfo(deconstructedPath.Last().Type, $"{name}{deconstructedPath.Aggregate(string.Empty, (previous, current) => previous + current.Name)}"));
            }
        }

        public static bool TryDeconstruct(ILogger<IValidatable> logger, IMetaInfo info, ref List<IMemberInfo> values)
        {
            if (!info.HasAttribute<DeconstructAttribute>())
                return false;

            if (!(info is IMemberInfo memberInfo))
                return false;

            var deconstruct = info.GetAttribute<DeconstructAttribute>();
            Deconstruct(logger, memberInfo.Type, info.Name, deconstruct.Properties, ref values);

            return true;
        }
    }
}