using UnityEngine;
using UnityEngine.Events;

namespace USFB
{
    public class SaveFileInput : FileInputBase
    {
        [SerializeField, Tooltip("Default file name")]
        private string _defaultName;

        [SerializeField, Tooltip("File extension filter. Example: 'png, jpg, jpeg'")]
        private string _accept;

        [Header("Events")]
        public UnityEvent<FileReference> OnFileSelected;

        public FileReference Value { get; private set; }

        protected override void OpenFilePanelAsync() =>
            StandaloneFileBrowser.SaveFilePanelAsync(_title, _directory, _defaultName, _accept, OnFileSelectedHandler);

        private void OnFileSelectedHandler(FileReference fileRef)
        {
            Value = fileRef;

            if (_text)
            {
                _text.text = fileRef is null ? "No file chosen" : fileRef.PathOrUrl;
            }

            OnFileSelected?.Invoke(fileRef);
        }
    }
}