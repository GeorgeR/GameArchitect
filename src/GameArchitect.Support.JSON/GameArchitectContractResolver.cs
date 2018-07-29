using System;
using System.Collections.Generic;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Attributes.Db;
using GameArchitect.Design.Attributes.Net;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using GameArchitect.Support.JSON.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameArchitect.Support.JSON
{
    public class GameArchitectContractResolver : DefaultContractResolver
    {
        private IDictionary<Type, JsonObjectContract> Cache { get; } = new Dictionary<Type, JsonObjectContract>();

        public override JsonContract ResolveContract(Type type)
        {
            return type.HasAttribute<ExportAttribute>() 
                ? GetOrCreateContract(type) 
                : base.ResolveContract(type);
        }

        private JsonObjectContract GetOrCreateContract(Type type)
        {
            if (Cache.ContainsKey(type))
                return Cache[type];

            var contract = new JsonObjectContract(type);

            contract.Properties.ForEach(p =>
            {
                if (p.AttributeProvider.HasAttribute<DbReferenceAttribute>())
                {
                    p.IsReference = true;
                    p.ItemIsReference = true;
                }

                if (p.AttributeProvider.HasAttribute<NetPropertyAttribute>())
                {
                    var attr = p.AttributeProvider.GetAttribute<NetPropertyAttribute>();
                    if (attr.Index.HasValue)
                        p.Order = attr.Index.Value;
                }

                if (p.AttributeProvider.HasAttribute<DefaultAttribute>())
                {
                    var attr = p.AttributeProvider.GetAttribute<DefaultAttribute>();
                    p.DefaultValue = attr.Value;
                    p.DefaultValueHandling = DefaultValueHandling.Populate;
                }
            });
            
            Cache.Add(type, contract);

            return contract;
        }
    }
}