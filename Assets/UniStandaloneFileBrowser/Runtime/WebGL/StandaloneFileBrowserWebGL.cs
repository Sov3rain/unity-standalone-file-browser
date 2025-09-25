#if UNITY_WEBGL || UNITY_EDITOR

using System;
using System.Linq;
using System.Threading.Tasks;

namespace USFB
{
    public class StandaloneFileBrowserWebGL : IStandaloneFileBrowser
    {
        public FileReference[] OpenFilePanel(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect)
        {
            var tcs = new TaskCompletionSource<FileReference[]>();

            WebGLCallbackReceiver.Instance.UploadFile(
                filter: GetBrowserFormattedFilter(extensions),
                multiselect: multiselect,
                callback: tcs.SetResult
            );

            // Blocking call
            return tcs.Task.GetAwaiter().GetResult();
        }

        public void OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileReference[]> callback)
        {
            WebGLCallbackReceiver.Instance.UploadFile(GetBrowserFormattedFilter(extensions), multiselect, callback);
        }

        public FileReference SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            throw new NotImplementedException($"{nameof(SaveFilePanel)} is not available for WebGL.");
        }

        public void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<FileReference> callback)
        {
            throw new NotImplementedException($"{nameof(SaveFilePanelAsync)} is not available for WebGL.");
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            throw new NotImplementedException($"{nameof(OpenFolderPanel)} is not available for WebGL.");
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            throw new NotImplementedException($"{nameof(OpenFolderPanelAsync)} is not available for WebGL.");
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
    }
}
#endif