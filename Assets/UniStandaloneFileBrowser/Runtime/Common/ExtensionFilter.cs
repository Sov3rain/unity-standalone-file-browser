using System;

namespace USFB
{
    public struct ExtensionFilter
    {
        public readonly string Name;
        public readonly string[] Extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions ?? Array.Empty<string>();
        }
    }
}