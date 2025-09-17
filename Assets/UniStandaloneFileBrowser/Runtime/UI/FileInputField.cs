using UnityEngine;
using UnityEngine.UI;
using USFB;

public class FileInputField : Button
{
    protected override void Awake()
    {
        onClick.AddListener(() =>
        {
            StandaloneFileBrowser.OpenFilePanelAsync("Title", "", "", false,
                (string[] paths) => { Debug.Log(paths[0]); });
        });
    }
}