using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace USFB
{
    public class OpenFileInput : MonoBehaviour
    {
        [Header("File Input Settings")]
        [SerializeField] private bool _multiselect;
        [SerializeField] private string _accept;

        [Header("UI Elements")]
        [SerializeField] Button _button;
        [SerializeField] TMP_Text _text;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", _accept, _multiselect, OnFileSelected);
        }

        private void OnFileSelected(FileInfo[] infos)
        {
            if (infos.Length == 0)
            {
                return;
            }

            _text.text = infos.Length > 1
                ? $"{infos.Length} files"
                : infos[0].FullName;
        }
    }
}