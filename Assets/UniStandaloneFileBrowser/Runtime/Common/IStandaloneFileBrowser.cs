using System;

namespace USFB
{
    public interface IStandaloneFileBrowser
    {
        FileReference[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

        string[] OpenFolderPanel(string title, string directory, bool multiselect);

        FileReference SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

        void OpenFilePanelAsync(
            string title,
            string directory,
            ExtensionFilter[] extensions,
            bool multiselect,
            Action<FileReference[]> callback);

        void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb);

        void SaveFilePanelAsync(
            string title,
            string directory,
            string defaultName,
            ExtensionFilter[] extensions,
            Action<FileReference> callback);
    }
}