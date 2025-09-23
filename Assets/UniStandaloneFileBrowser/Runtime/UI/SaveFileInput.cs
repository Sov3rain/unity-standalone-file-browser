using System.IO;
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
        public UnityEvent<FileInfo> OnFileSelected;

        public FileInfo Value { get; private set; }

        private void OnEnable()
        {
            if (!_button) return;
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            if (!_button) return;
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            StandaloneFileBrowser.SaveFilePanelAsync(_title, _directory, _defaultName, _accept, OnFileSelectedHandler);
        }

        private void OnFileSelectedHandler(FileInfo info)
        {
            Value = info;

            if (_text)
            {
                _text.text = info is null ? "No file chosen" : info.FullName;
            }

            OnFileSelected?.Invoke(info);
        }
    }
}