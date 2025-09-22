using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace USFB
{
    public struct ExtensionFilter
    {
        private static readonly Regex VALID_EXTENSION_REGEX = new("^[a-zA-Z0-9]+$");

        public readonly string Name;
        public readonly string[] Extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions?
                             .Select(e => e.TrimStart('.').Trim().ToLowerInvariant())
                             .Where(e => !string.IsNullOrWhiteSpace(e) && VALID_EXTENSION_REGEX.IsMatch(e))
                             .Distinct()
                             .ToArray()
                         ?? Array.Empty<string>();
        }
    }
}