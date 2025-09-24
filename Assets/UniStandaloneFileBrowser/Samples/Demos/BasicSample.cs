// using System.IO;
// using UnityEngine;
// using USFB;
//
// public class BasicSample : MonoBehaviour
// {
//     private string _output;
//
//     void OnGUI()
//     {
//         var guiScale = new Vector3(Screen.width / 800.0f, Screen.height / 600.0f, 1.0f);
//         GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, guiScale);
//
//         GUILayout.Space(20);
//         GUILayout.BeginHorizontal();
//         GUILayout.Space(20);
//         GUILayout.BeginVertical();
//
//         // Open File Samples
//
//         if (GUILayout.Button("Open File"))
//         {
//             WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Async"))
//         {
//             // Example with callback - processes the result when dialog completes
//             StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", "", false, paths => { WriteResult(paths); });
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Async (No Callback)"))
//         {
//             // Example without callback - fire-and-forget, callback parameter is optional
//             StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", "", false, null);
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Multiple"))
//         {
//             WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Extension"))
//         {
//             WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "txt", true));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Directory"))
//         {
//             WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", Application.dataPath, "", true));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open File Filter"))
//         {
//             var extensions = new[]
//             {
//                 new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
//                 new ExtensionFilter("Sound Files", "mp3", "wav"),
//                 new ExtensionFilter("All Files", "*"),
//             };
//             WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true));
//         }
//
//         GUILayout.Space(15);
//
//         // Open Folder Samples
//
//         if (GUILayout.Button("Open Folder"))
//         {
//             var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
//             WriteResult(paths);
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open Folder Async"))
//         {
//             // Example with callback - processes the result when dialog completes
//             StandaloneFileBrowser.OpenFolderPanelAsync("Select Folder", "", true, paths => { WriteResult(paths); });
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open Folder Async (No Callback)"))
//         {
//             // Example without callback - fire-and-forget, callback parameter is optional
//             StandaloneFileBrowser.OpenFolderPanelAsync("Select Folder", "", true, null);
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Open Folder Directory"))
//         {
//             var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", Application.dataPath, true);
//             WriteResult(paths);
//         }
//
//         GUILayout.Space(15);
//
//         // Save File Samples
//
//         if (GUILayout.Button("Save File"))
//         {
//             WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "", ""));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Async"))
//         {
//             // Example with callback - processes the result when dialog completes
//             StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "", path => { WriteResult(path); });
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Async (No Callback)"))
//         {
//             // Example without callback - fire-and-forget, callback parameter is optional
//             StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "", null);
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Default Name"))
//         {
//             WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", ""));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Default Name Ext"))
//         {
//             WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", "dat"));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Directory"))
//         {
//             WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "", ""));
//         }
//
//         GUILayout.Space(5);
//         if (GUILayout.Button("Save File Filter"))
//         {
//             // Multiple save extension filters with more than one extension support.
//             var extensionList = new[]
//             {
//                 new ExtensionFilter("Binary", "bin"),
//                 new ExtensionFilter("Text", "txt"),
//             };
//             WriteResult(StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList));
//         }
//
//         GUILayout.EndVertical();
//         GUILayout.Space(20);
//         GUILayout.Label(_output);
//         GUILayout.EndHorizontal();
//     }
//
//     public void WriteResult(FileReference[] infos)
//     {
//         if (infos.Length == 0)
//         {
//             return;
//         }
//
//         _output = null;
//         foreach (var p in infos)
//         {
//             _output += p + "\n";
//         }
//     }
//
//     public void WriteResult(FileReference info)
//     {
//         _output = info.ToString();
//     }
// }