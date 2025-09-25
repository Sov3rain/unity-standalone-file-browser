#if UNITY_WEBGL || UNITY_EDITOR

using System;
using System.Linq;

namespace USFB
{
    public class StandaloneFileBrowserWebGL : IStandaloneFileBrowser
    {
        public FileReference[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            throw new NotImplementedException();
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            throw new NotImplementedException();
        }

        public string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            throw new NotImplementedException();
        }

        public void OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileReference[]> cb)
        {
            WebGLCallbackReceiver.Instance.UploadFile(GetBrowserFormattedFilter(extensions), multiselect, cb);
        }

        private string GetBrowserFormattedFilter(ExtensionFilter[] extensions)
        {
            if (extensions == null)
            {
                return "";
            }

            return extensions
                .SelectMany(ext => ext.Extensions)
                .Aggregate("", (current, fil) => current + $".{fil}, ");
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            throw new NotImplementedException();
        }

        public void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<string> cb)
        {
            throw new NotImplementedException();
        }
    }
}
#endif