using System;
using System.IO;

namespace USFB
{
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
#endif
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns an array of chosen paths. Zero length array when canceled</returns>
        public static FileInfo[] OpenFilePanel(string title, string directory, string extension, bool multiselect)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            return OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns an array of chosen paths. Zero length array when canceled</returns>
        public static FileInfo[] OpenFilePanel(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect)
        {
            var paths = _platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
            var fileInfos = new FileInfo[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                fileInfos[i] = new FileInfo(paths[i]);
            }

            return fileInfos;
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback</param>
        public static void OpenFilePanelAsync(
            string title,
            string directory,
            string extension,
            bool multiselect,
            Action<FileInfo[]> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback</param>
        public static void OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileInfo[]> cb)
        {
            void CallbackWrapper(string[] paths)
            {
                var fileInfos = new FileInfo[paths.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    fileInfos[i] = new FileInfo(paths[i]);
                }

                cb.Invoke(fileInfos);
            }

            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, CallbackWrapper);
        }

        /// <summary>
        /// Native open folder dialog
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <returns>Returns an array of chosen paths. Zero length array when canceled</returns>
        public static DirectoryInfo[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            var paths = _platformWrapper.OpenFolderPanel(title, directory, multiselect);
            var directoryInfos = new DirectoryInfo[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                directoryInfos[i] = new DirectoryInfo(paths[i]);
            }

            return directoryInfos;
        }

        /// <summary>
        /// Native open folder dialog async
        /// NOTE: Multiple folder selection not supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <param name="cb">Callback</param>
        public static void OpenFolderPanelAsync(
            string title,
            string directory,
            bool multiselect,
            Action<DirectoryInfo[]> cb)
        {
            void CallbackWrapper(string[] paths)
            {
                var directoryInfos = new DirectoryInfo[paths.Length];
                for (int i = 0; i < paths.Length; i++)
                {
                    directoryInfos[i] = new DirectoryInfo(paths[i]);
                }

                cb.Invoke(directoryInfos);
            }

            _platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, CallbackWrapper);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <returns>Returns the chosen path. Empty string when canceled</returns>
        public static FileInfo SaveFilePanel(string title, string directory, string defaultName, string extension)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            return SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <returns>Returns the chosen path. Empty string when canceled</returns>
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
        /// <param name="extension">File extension</param>
        /// <param name="cb">Callback</param>
        public static void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            string extension,
            Action<FileInfo> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="cb">Callback</param>
        public static void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<FileInfo> cb)
        {
            void CallbackWrapper(string path)
            {
                cb.Invoke(string.IsNullOrEmpty(path) ? null : new FileInfo(path));
            }

            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, CallbackWrapper);
        }
    }
}