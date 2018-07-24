using System;
using System.Collections.Generic;
using System.Linq;

namespace GameArchitect.Design.Support.Attributes
{
    /// <summary>
    /// A generic tag container containing one or more strings or 
    /// key/value pairs.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class TagAttribute : Attribute
    {
        public List<string> Tags { get; }
        private Dictionary<string, string> Pairs { get; } = new Dictionary<string, string>();

        public TagAttribute(params string[] tags)
        {
            Tags = tags.ToList();
        }

        private void Decompose()
        {
            if (Pairs.Count == 0 && Tags.Count > 0)
            {
                foreach (var tag in Tags)
                {
                    var split = tag.Split('=', ':');
                    if(split.Length > 1)
                        Pairs.Add(split[0], split[1]);

                    Pairs.Add(split[0], null);
                }
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
    }
}