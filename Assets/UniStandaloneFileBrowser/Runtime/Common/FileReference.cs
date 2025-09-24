using System;
using System.IO;

[Serializable]
public sealed class FileReference
{
    public string PathOrUrl { get; }
    public bool IsUrl { get; }

    private FileReference(string pathOrUrl, bool isUrl)
    {
        PathOrUrl = pathOrUrl;
        IsUrl = isUrl;
    }

    public static FileReference FromPath(string path)
    {
        return new(path, false);
    }

    public static FileReference FromUrl(string url)
    {
        return new(url, true);
    }

    public FileInfo ToFileInfo() => IsUrl ? null : new(PathOrUrl);
    public Uri ToUri() => !IsUrl ? null : new(PathOrUrl);
}