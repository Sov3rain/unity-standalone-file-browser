using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLCallbackReceiver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public static WebGLCallbackReceiver Instance { get; private set; }

    private Action<string[]> _callback;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        Instance = new GameObject(nameof(WebGLCallbackReceiver)).AddComponent<WebGLCallbackReceiver>();
        DontDestroyOnLoad(Instance.gameObject);
        Instance.gameObject.hideFlags = HideFlags.HideAndDontSave;
    }

    // public string[] OpenFilePanel(string title, string directory, bool multiselect)
    // {

    // }

    public void OpenFilePanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
    {
        Debug.Log($"OpenFilePanelAsync: {title}, {directory}, {multiselect}");
        UploadFile(gameObject.name, nameof(OnBrowserCallbackHandler), "", multiselect);
        _callback = cb;
    }

    public void OnBrowserCallbackHandler(string url)
    {
        _callback(new[] { url });
        Debug.Log(url);
    }
}