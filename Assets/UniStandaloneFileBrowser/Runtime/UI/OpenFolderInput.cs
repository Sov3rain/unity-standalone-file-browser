using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace USFB
{
    public class OpenFolderInput : MonoBehaviour
    {
        [Header("Folder Input Settings")]
        [SerializeField, Tooltip("Dialog title")]
        private string _title;
        
        [SerializeField, Tooltip("Root directory")]
        private string _directory;
        
        [SerializeField, Tooltip("Allow multiple folder selection, not supported in Editor mode.")]
        private bool _multiselect;
        
        [Header("UI Elements")]
        [SerializeField] Button _button;
        [SerializeField] TMP_Text _text;
        
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