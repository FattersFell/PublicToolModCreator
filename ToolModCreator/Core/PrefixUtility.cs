using System;

namespace Editor.ToolModCreator.Core
{
    /// <summary>Applies normalized prefixes to generated names.</summary>
    public static class PrefixUtility
    {
        public static string Apply(string prefix, string value)
        {
            string trimmed = (prefix ?? "").Trim();

            if (trimmed.Length == 0)
            {
                return value;
            }

            if (value.Equals(trimmed, StringComparison.OrdinalIgnoreCase) ||
                value.StartsWith(trimmed + " ", StringComparison.OrdinalIgnoreCase))
            {
                return value;
            }

            return trimmed + " " + value;
        }
    }
}