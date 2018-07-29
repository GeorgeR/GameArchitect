using System;
using System.Collections.Generic;
using System.Linq;
using GameArchitect.Design.Metadata;

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

        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            ForMeta<IMemberInfo>(info, o =>
            {
                var type = o.Type;
                if (Properties == null || Properties.Count == 0)
                {
                    if (type.ImplementsInterface<IDeconstructible>())
                        Properties = type.Create<IDeconstructible>().Deconstruct().ToList();
                    else
                        throw new Exception($"A Deconstruct attribute is specified for {o.GetPath()} but no Properties were provided, and the type {type.GetPath()} does not implement IDeconstructible.");
                }

                var typeProperties = type.GetProperties()
                    .Select(p => p.Name)
                    .ToList();

                if (!Properties.All(p => typeProperties.Contains(p)))
                    throw new Exception($"One or more properties specified for a deconstructible were not found on the actual type. Note that the names are case sensitive.");
            });

            return true;
        }

        private static void DeconstructPath(TypeInfo type, string path, ref List<IMemberInfo> result)
        {
            if (path.Length == 0)
                return;

            var actualProperties = type.GetProperties();
            var propertyName = path.Split('.')[0];

            var actualProperty = actualProperties.FirstOrDefault(o => o.Name == propertyName);
            if (actualProperty == null)
                throw new NullReferenceException($"Tried to deconstruct with invalid property name ({propertyName}) on type {type.GetPath()}.");

            // TODO
            //result.Add(new MemberInfo(actualProperty.Type, propertyName));

            if (!path.Contains('.'))
                return;

            path = path.Substring(path.IndexOf('.') + 1);
            DeconstructPath(actualProperty.Type, path, ref result);
        }

        private static void Deconstruct(TypeInfo type, string name, List<string> deconstructionProperties, ref List<IMemberInfo> result)
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
                DeconstructPath(type, propertyPath, ref deconstructedPath);

                // TODO
                //result.Add(new MemberInfo(deconstructedPath.Last().Type, $"{name}{deconstructedPath.Aggregate(string.Empty, (previous, current) => previous + current.Name)}"));
            }
        }

        public static bool TryDeconstruct(IMetaInfo info, ref List<IMemberInfo> values)
        {
            if (!info.HasAttribute<DeconstructAttribute>())
                return false;

            if (!(info is IMemberInfo memberInfo))
                return false;

            var deconstruct = info.GetAttribute<DeconstructAttribute>();
            Deconstruct(memberInfo.Type, info.Name, deconstruct.Properties, ref values);

            return true;
        }
    }
}