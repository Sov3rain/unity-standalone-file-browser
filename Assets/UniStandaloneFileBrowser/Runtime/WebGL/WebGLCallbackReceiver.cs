using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace USFB
{
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
            Instance.gameObject.hideFlags = HideFlags.NotEditable;
            DontDestroyOnLoad(Instance.gameObject);
        }

        public void UploadFile(string filter, bool multiselect, Action<string[]> cb)
        {
            _callback = cb;
            UploadFile(gameObject.name, nameof(OnBrowserMessageHandler), filter, multiselect);
        }

        // Called from the browser using SendMessage
        public void OnBrowserMessageHandler(string urls)
        {
            string[] urlArr = urls?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            _callback?.Invoke(urlArr);
            Debug.Log(urls);
            _callback = null;
        }
    }
}