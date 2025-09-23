using System.IO;
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
        public UnityEvent<FileInfo[]> OnFileSelected;

        public FileInfo[] Value { get; private set; }

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
            StandaloneFileBrowser.OpenFilePanelAsync(_title, _directory, _accept, _multiselect, OnFileSelectedHandler);
        }

        private void OnFileSelectedHandler(FileInfo[] infos)
        {
            Value = infos;

            if (_text)
            {
                _text.text = infos.Length switch
                {
                    > 1 => $"{infos.Length} files",
                    1 => infos.ElementAtOrDefault(0)?.FullName,
                    _ => "No file chosen",
                };
            }

            OnFileSelected?.Invoke(infos);
        }
    }
}