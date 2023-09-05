using System;
using System.Threading.Tasks;

namespace USFB
{
    public class StandaloneFileBrowser
    {
        private static IStandaloneFileBrowser _platformWrapper = null;

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
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFilePanel(string title, string directory, string extension, bool multiselect)
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
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFilePanel(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect)
        {
            return _platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog async using delegates
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanel(
            string title,
            string directory,
            string extension,
            bool multiselect,
            Action<string[]> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            OpenFilePanel(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open file dialog async using delegates
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanel(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<string[]> cb)
        {
            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory"></param>
        /// <param name="extensions"></param>
        /// <param name="multiselect"></param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static Task<string[]> OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect)
        {
            var task = new TaskCompletionSource<string[]>();
            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, task.SetResult);
            return task.Task;
        }

        public static Task<string[]> OpenFilePanelAsync(
            string title,
            string directory,
            string extension,
            bool multiselect)
        {
            var task = new TaskCompletionSource<string[]>();
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            _platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, task.SetResult);
            return task.Task;
        }

        /// <summary>
        /// Native open folder dialog
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            return _platformWrapper.OpenFolderPanel(title, directory, multiselect);
        }

        /// <summary>
        /// Native open folder dialog async using delegates
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <param name="cb">Callback")</param>
        public static void OpenFolderPanel(string title, string directory, bool multiselect, Action<string[]> cb)
        {
            _platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
        }

        /// <summary>
        /// Native open folder dialog async
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory"></param>
        /// <param name="multiselect"></param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static Task<string[]> OpenFolderPanelAsync(string title, string directory, bool multiselect)
        {
            var task = new TaskCompletionSource<string[]>();
            _platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, task.SetResult);
            return task.Task;
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static string SaveFilePanel(string title, string directory, string defaultName, string extension)
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
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static string SaveFilePanel(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions)
        {
            return _platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog async using delegates
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanel(
            string title,
            string directory,
            string defaultName,
            string extension,
            Action<string> cb)
        {
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            SaveFilePanel(title, directory, defaultName, extensions, cb);
        }

        /// <summary>
        /// Native save file dialog async using delegates
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanel(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<string> cb)
        {
            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory"></param>
        /// <param name="defaultName"></param>
        /// <param name="extensions"></param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static Task<string> SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions)
        {
            var task = new TaskCompletionSource<string>();
            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, task.SetResult);
            return task.Task;
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory"></param>
        /// <param name="defaultName"></param>
        /// <param name="extension"></param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static Task<string> SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            string extension)
        {
            var task = new TaskCompletionSource<string>();
            var extensions = string.IsNullOrEmpty(extension) ? null : new[] { new ExtensionFilter("", extension) };
            _platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, task.SetResult);
            return task.Task;
        }
    }
}