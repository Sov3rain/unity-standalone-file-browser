using System;
using System.IO;
using System.Linq;

namespace USFB
{
    /// <summary>
    /// Cross-platform file browser API that provides native file dialogs
    /// and returns strongly typed FileInfo and DirectoryInfo objects
    /// </summary>
    public static class StandaloneFileBrowser
    {
        private static readonly IStandaloneFileBrowser _platformWrapper;

        static StandaloneFileBrowser()
        {
#if UNITY_EDITOR
            _platformWrapper = new StandaloneFileBrowserEditor();
#elif UNITY_STANDALONE_OSX
            _platformWrapper = new StandaloneFileBrowserMac();
#elif UNITY_STANDALONE_WIN
            _platformWrapper = new StandaloneFileBrowserWindows();
#elif UNITY_STANDALONE_LINUX
            _platformWrapper = new StandaloneFileBrowserLinux();
#elif UNITY_WEBGL
            _platformWrapper = new StandaloneFileBrowserWebGL();
#endif
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">Allowed extension, comma separated; null or empty allows all files</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>An array of selected files; empty when canceled</returns>
        public static FileReference[] OpenFilePanel(string title, string directory, string extensions, bool multiselect)
        {
            return OpenFilePanel(title, directory, GetExtensionFilters(extensions), multiselect);
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters; null allows all files. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>An array of selected files; empty when canceled</returns>
        public static FileReference[] OpenFilePanel(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect)
        {
            return _platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">Allowed extension, comma separated; null or empty allows all files</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="callback">Optional callback invoked with the selected files (empty array on cancel); can be null</param>
        public static void OpenFilePanelAsync(
            string title,
            string directory,
            string extensions,
            bool multiselect,
            Action<FileReference[]> callback)
        {
            OpenFilePanelAsync(title, directory, GetExtensionFilters(extensions), multiselect, callback);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters; null allows all files. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="callback">Optional callback invoked with the selected files (empty array on cancel); can be null</param>
        public static void OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileReference[]> callback)
        {
            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, callback);
        }

        /// <summary>
        /// Native open folder dialog
        /// NOTE: Multiple folder selection is not supported on Windows
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect">Allow multiple folder selection</param>
        /// <returns>An array of selected directories; empty when canceled</returns>
        public static DirectoryInfo[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            var paths = _platformWrapper.OpenFolderPanel(title, directory, multiselect) ?? Array.Empty<string>();
            var directoryInfos = new DirectoryInfo[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                directoryInfos[i] = new DirectoryInfo(paths[i]);
            }

            return directoryInfos;
        }

        /// <summary>
        /// Native open folder dialog async
        /// NOTE: Multiple folder selection is not supported on Windows
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect">Allow multiple folder selection</param>
        /// <param name="callback">Optional callback invoked with the selected directories (empty array on cancel); can be null</param>
        public static void OpenFolderPanelAsync(
            string title,
            string directory,
            bool multiselect,
            Action<DirectoryInfo[]> callback)
        {
            void CallbackWrapper(string[] paths)
            {
                var pathsArray = paths ?? Array.Empty<string>();
                var directoryInfos = new DirectoryInfo[pathsArray.Length];
                for (int i = 0; i < pathsArray.Length; i++)
                {
                    directoryInfos[i] = new DirectoryInfo(pathsArray[i]);
                }

                callback?.Invoke(directoryInfos);
            }

            _platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, CallbackWrapper);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">File extensions, comma separated; null or empty allows all files</param>
        /// <returns>The chosen FileInfo; null when canceled</returns>
        public static FileInfo SaveFilePanel(string title, string directory, string defaultName, string extensions)
        {
            return SaveFilePanel(title, directory, defaultName, GetExtensionFilters(extensions));
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters; null allows all files. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <returns>The chosen FileInfo; null when canceled</returns>
        public static FileInfo SaveFilePanel(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions)
        {
            var path = _platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
            return string.IsNullOrEmpty(path) ? null : new FileInfo(path);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">File extensions, comma separated; null or empty allows all files</param>
        /// <param name="callback">Optional callback invoked with the chosen FileInfo (null on cancel); can be null</param>
        public static void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            string extensions,
            Action<FileInfo> callback)
        {
            SaveFilePanelAsync(title, directory, defaultName, GetExtensionFilters(extensions), callback);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters; null allows all files. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="callback">Optional callback invoked with the chosen FileInfo (null on cancel); can be null</param>
        public static void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<FileInfo> callback)
        {
            void CallbackWrapper(string path)
            {
                callback?.Invoke(string.IsNullOrEmpty(path) ? null : new FileInfo(path));
            }

            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, CallbackWrapper);
        }

        public static ExtensionFilter[] GetExtensionFilters(string input) =>
            input?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(ext => ext.Trim())
                .Where(ext => ext.Length > 0)
                .Select(ext => new ExtensionFilter(ext.ToUpperInvariant(), ext))
                .ToArray()
            ?? Array.Empty<ExtensionFilter>();
    }
}