using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace USFB
{
    [DisallowMultipleComponent]
    public abstract class FileInputBase : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] protected Button _button;
        [SerializeField] protected TMP_Text _text;

        [Header("File Input Settings")]
        [SerializeField, Tooltip("Dialog title")]
        protected string _title;

        [SerializeField, Tooltip("Root directory")]
        protected string _directory;

        private EventTrigger _eventTrigger;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _eventTrigger = _button.gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            entry.callback.AddListener(_ => OpenFilePanelAsync());
            _eventTrigger.triggers.Add(entry);
#endif
        }

        private void OnEnable()
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_EDITOR
            if (!_button) return;
            _button.onClick.AddListener(OpenFilePanelAsync);
#endif
        }

        private void OnDisable()
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX || UNITY_EDITOR
            if (!_button) return;
            _button.onClick.RemoveListener(OpenFilePanelAsync);
#endif
        }

        protected abstract void OpenFilePanelAsync();
    }
}