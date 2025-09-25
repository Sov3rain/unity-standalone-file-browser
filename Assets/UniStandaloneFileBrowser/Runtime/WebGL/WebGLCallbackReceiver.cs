using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace USFB
{
    public class WebGLCallbackReceiver : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

        public static WebGLCallbackReceiver Instance { get; private set; }

        private Action<FileReference[]> _callback;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            // Create a singleton instance of WebGLCallbackReceiver
#if UNITY_WEBGL && !UNITY_EDITOR
            Instance = new GameObject(nameof(WebGLCallbackReceiver)).AddComponent<WebGLCallbackReceiver>();
            Instance.gameObject.hideFlags = HideFlags.NotEditable;
            DontDestroyOnLoad(Instance.gameObject);
#endif
        }

        public void UploadFile(string filter, bool multiselect, Action<FileReference[]> cb)
        {
            _callback = cb;
            UploadFile(gameObject.name, nameof(OnBrowserMessageHandler), filter, multiselect);
        }

        // Called from the browser using SendMessage
        public void OnBrowserMessageHandler(string urls)
        {
            var urlArr = urls?.Split(',')?.Select(FileReference.FromUrl).ToArray()
                         ?? Array.Empty<FileReference>();

            Debug.Log(urls);

            _callback?.Invoke(urlArr);
            _callback = null;
        }
    }
}