using System;
using System.Runtime.InteropServices;

namespace USFB
{
    public class StandaloneFileBrowserWebGL : IStandaloneFileBrowser
    {
        public string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
        {
            // UploadFile(
            //     gameObjectName: nameof(WebGLCallbackReceiver),
            //     methodName: nameof(WebGLCallbackReceiver.OnBrowserCallbackHandler),
            //     filter: ".png, .jpg",
            //     multiple: multiselect
            // );
            // return Array.Empty<string>();
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
            Action<string[]> cb)
        {
            WebGLCallbackReceiver.Instance.OpenFilePanelAsync(multiselect, cb);
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
        }

        public void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions,
            Action<string> cb)
        {
            throw new NotImplementedException();
        }
    }
}