using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace USFB
{
    public struct ExtensionFilter
    {
        private static readonly Regex ValidExtensionRegex = new("^[a-zA-Z0-9]+$");

        public readonly string Name;
        public readonly string[] Extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            if (filterExtensions is null)
            {
                Name = filterName;
                Extensions = Array.Empty<string>();
                return;
            }

            foreach (var ext in filterExtensions)
            {
                var clean = ext?.TrimStart('.')?.Trim();
                if (string.IsNullOrEmpty(clean))
                    throw new ArgumentException("Extension cannot be empty or only whitespace.");

                if (!ValidExtensionRegex.IsMatch(clean))
                    throw new ArgumentException($"Invalid file extension: '{ext}'");
            }

            Name = filterName;
            Extensions = filterExtensions
                .Distinct()
                .Select(e => e.TrimStart('.').Trim().ToLowerInvariant())
                .ToArray();
        }
    }
}