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

        [DllImport("__Internal")]
        private static extern void DownloadFile(
            string gameObjectName,
            string methodName,
            string filename,
            byte[] byteArray,
            int byteArraySize);

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

        public void UploadFile(string filter, bool multiselect, Action<FileReference[]> callback)
        {
            _callback = callback;
            UploadFile(gameObject.name, nameof(OnUploadComplete), filter, multiselect);
        }

        public void DownloadFile(string filename, byte[] data)
        {
            DownloadFile(gameObject.name, nameof(OnDownloadComplete), filename, data, data.Length);
        }

        // Called from the browser using SendMessage
        public void OnUploadComplete(string urls)
        {
            FileReference[] fileRefs = urls?.Split(',')?.Select(FileReference.FromUrl).ToArray()
                                     ?? Array.Empty<FileReference>();

            _callback?.Invoke(fileRefs);
            _callback = null;
        }

        public void OnDownloadComplete(string _)
        {
        }
    }
}