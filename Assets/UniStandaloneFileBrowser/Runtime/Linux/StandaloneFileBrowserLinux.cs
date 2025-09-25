#if UNITY_STANDALONE_LINUX || UNITY_EDITOR

using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace USFB
{
    public class StandaloneFileBrowserLinux : IStandaloneFileBrowser
    {
        private static Action<string[]> _openFileCb;
        private static Action<string[]> _openFolderCb;
        private static Action<string> _saveFileCb;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void AsyncCallback(string path);

        [DllImport("StandaloneFileBrowser")]
        private static extern void DialogInit();

        [DllImport("StandaloneFileBrowser")]
        private static extern IntPtr DialogOpenFilePanel(string title, string directory, string extension,
            bool multiselect);

        [DllImport("StandaloneFileBrowser")]
        private static extern void DialogOpenFilePanelAsync(string title, string directory, string extension,
            bool multiselect, AsyncCallback callback);

        [DllImport("StandaloneFileBrowser")]
        private static extern IntPtr DialogOpenFolderPanel(string title, string directory, bool multiselect);

        [DllImport("StandaloneFileBrowser")]
        private static extern void DialogOpenFolderPanelAsync(string title, string directory, bool multiselect,
            AsyncCallback callback);

        [DllImport("StandaloneFileBrowser")]
        private static extern IntPtr DialogSaveFilePanel(string title, string directory, string defaultName,
            string extension);

        [DllImport("StandaloneFileBrowser")]
        private static extern void DialogSaveFilePanelAsync(string title, string directory, string defaultName,
            string extension, AsyncCallback callback);

        private const char FILE_SEPARATOR = (char)28;

        public StandaloneFileBrowserLinux()
        {
            DialogInit();
        }

        public FileReference[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions,
            bool multiselect)
        {
            var paths = Marshal.PtrToStringAnsi(DialogOpenFilePanel(
                title,
                directory,
                GetFilterFromFileExtensionList(extensions),
                multiselect));

            return paths?.Split(FILE_SEPARATOR).Select(FileReference.FromPath).ToArray() ??
                   Array.Empty<FileReference>();
        }

        public void OpenFilePanelAsync(
            string title, 
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileReference[]> callback)
        {
            // _openFileCb = cb;
            _openFileCb = CallbackWrapper;
            
            void CallbackWrapper(string[] paths)
            {
                callback?.Invoke(paths?.Select(FileReference.FromPath).ToArray() ?? Array.Empty<FileReference>());
            }
            
            if (callback != null)
            {
                DialogOpenFilePanelAsync(
                    title,
                    directory,
                    GetFilterFromFileExtensionList(extensions),
                    multiselect,
                    result => { _openFileCb?.Invoke(result.Split(FILE_SEPARATOR)); });
            }
        }

        public string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            var paths = Marshal.PtrToStringAnsi(DialogOpenFolderPanel(
                title,
                directory,
                multiselect));
            return paths.Split(FILE_SEPARATOR);
        }

        public void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            _openFolderCb = cb;
            if (cb != null)
            {
                DialogOpenFolderPanelAsync(
                    title,
                    directory,
                    multiselect,
                    (string result) => { _openFolderCb?.Invoke(result.Split(FILE_SEPARATOR)); });
            }
        }

        public FileReference SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
        {
            string path = Marshal.PtrToStringAnsi(DialogSaveFilePanel(
                title,
                directory,
                defaultName,
                GetFilterFromFileExtensionList(extensions)));
            
            return FileReference.FromPath(path);
        }

        public void SaveFilePanelAsync(
            string title, 
            string directory, 
            string defaultName, 
            ExtensionFilter[] extensions,
            Action<FileReference> callback)
        {
            _saveFileCb = CallbackWrapper;
            
            void CallbackWrapper(string path)
            {
                callback?.Invoke(FileReference.FromPath(path));
            }
            
            if (callback != null)
            {
                DialogSaveFilePanelAsync(
                    title,
                    directory,
                    defaultName,
                    GetFilterFromFileExtensionList(extensions),
                    result => { _saveFileCb?.Invoke(result); });
            }
        }

        private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
        {
            if (extensions == null)
            {
                return "";
            }

            var filterString = "";
            foreach (var filter in extensions)
            {
                filterString += filter.Name + ";";

                foreach (var ext in filter.Extensions)
                {
                    filterString += ext + ",";
                }

                filterString = filterString.Remove(filterString.Length - 1);
                filterString += "|";
            }

            filterString = filterString.Remove(filterString.Length - 1);
            return filterString;
        }
    }
}

#endif