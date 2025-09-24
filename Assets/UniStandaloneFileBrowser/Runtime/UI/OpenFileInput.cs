using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace USFB
{
    public class OpenFileInput : FileInputBase
    {
        [SerializeField, Tooltip("File extension filter. Example: 'png, jpg, jpeg'")]
        private string _accept;

        [SerializeField, Tooltip("Allow multiple file selection, not supported in Editor mode.")]
        private bool _multiselect;

        [Header("Events")]
        public UnityEvent<FileReference[]> OnFileSelected;

        public FileReference[] Value { get; private set; }

        protected override void OpenFilePanelAsync() =>
            StandaloneFileBrowser.OpenFilePanelAsync(_title, _directory, _accept, _multiselect, OnFileSelectedHandler);

        private void OnFileSelectedHandler(FileReference[] infos)
        {
            Value = infos;

            if (_text)
            {
                _text.text = infos.Length switch
                {
                    > 1 => $"{infos.Length} files",
                    1 => infos.ElementAtOrDefault(0)?.PathOrUrl,
                    _ => "No file chosen",
                };
            }

            OnFileSelected?.Invoke(infos);
        }
    }
}