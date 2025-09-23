using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace USFB
{
    public class OpenFolderInput : FileInputBase
    {
        [SerializeField, Tooltip("Allow multiple folder selection, not supported in Editor mode.")]
        private bool _multiselect;
        
        [Header("Events")]
        public UnityEvent<DirectoryInfo[]> OnFolderSelected;
        
        public DirectoryInfo[] Value { get; private set; }
        
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
            StandaloneFileBrowser.OpenFolderPanelAsync(_title, _directory, _multiselect, OnFolderSelectedHandler);
        }
        
        private void OnFolderSelectedHandler(DirectoryInfo[] infos)
        {
            Value = infos;
            
            if (_text)
            {
                _text.text = infos.Length switch
                {
                    > 1 => $"{infos.Length} folders",
                    1 => infos.ElementAtOrDefault(0)?.FullName,
                    _ => "No folder chosen",
                };
            }
            
            OnFolderSelected?.Invoke(infos);
        }
    }
}