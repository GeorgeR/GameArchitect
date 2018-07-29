using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// A generic tag container containing one or more strings or 
    /// key/value pairs.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class TagAttribute : ValidatableAttribute
    {
        public List<string> Tags { get; } // Raw tags, actual tags are stored in Pairs after Decompose call
        private Dictionary<string, string> Pairs { get; } = new Dictionary<string, string>();

        public TagAttribute(params string[] tags)
        {
            Tags = tags.ToList();
        }

        private void Decompose()
        {
            if (Pairs.Count != 0 || Tags.Count <= 0)
                return;

            foreach (var tag in Tags)
            {
                var split = tag.Split('=', ':');
                if(split.Length > 1)
                    Pairs.Add(split[0].Trim(' '), split[1].Trim(' '));

                Pairs.Add(split[0].Trim(' '), null);
            }
        }

        public bool HasTag(string tag)
        {
            Decompose();

            return Pairs.ContainsKey(tag.ToLower());
        }

        public bool TryGetValue(string key, out string value)
        {
            Decompose();
            value = string.Empty;

            if (!HasTag(key))
                return false;

            value = Pairs[key.ToLower()];
            return !string.IsNullOrEmpty(value);
        }

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            // TODO: Validate tag string format - check for illegal characters

            return true;
        }
    }
}