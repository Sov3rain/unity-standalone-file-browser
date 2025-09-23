using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class FileInputBase: MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] protected Button _button;
    [SerializeField] protected TMP_Text _text;
    
    [Header("File Input Settings")]
    [SerializeField, Tooltip("Dialog title")]
    protected string _title;

    [SerializeField, Tooltip("Root directory")]
    protected string _directory;
}