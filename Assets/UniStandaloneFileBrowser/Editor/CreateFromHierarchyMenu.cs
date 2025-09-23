using UnityEditor;
using UnityEngine;

namespace USFB
{
    public static class CreateFromHierarchyMenu
    {
        [MenuItem("GameObject/UI/Open File Input", false, 1)]
        private static void CreateOpenFileInput() => Create<OpenFileInput>(prefabName: "File Input Field");

        [MenuItem("GameObject/UI/Open Folder Input", false, 2)]
        private static void CreateOpenFolderInput() => Create<OpenFolderInput>(prefabName: "Folder Input Field");
        
        [MenuItem("GameObject/UI/Save File Input", false, 3)]
        private static void CreateSaveFileInput() => Create<SaveFileInput>(prefabName: "Save File Input Field");

        private static void Create<T>(string prefabName)
            where T : MonoBehaviour
        {
            var guids = AssetDatabase.FindAssets($"{prefabName} t:Prefab");

            if (guids.Length == 0)
            {
                Debug.LogError($"No prefab '{prefabName}' found in project.");
                return;
            }

            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            var prefab = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            var parent = Selection.activeGameObject;

            if (!parent || !parent.TryGetComponent<Canvas>(out _))
            {
                parent = Object.FindFirstObjectByType<Canvas>().gameObject;
            }

            T go = PrefabUtility.InstantiatePrefab(prefab) as T;

            if (!go)
            {
                Debug.LogError($"Failed to instantiate prefab '{prefabName}'.");
                return;
            }

            PrefabUtility.UnpackPrefabInstance(
                go.gameObject,
                PrefabUnpackMode.Completely,
                InteractionMode.AutomatedAction
            );

            if (parent)
            {
                GameObjectUtility.SetParentAndAlign(go.gameObject, parent);
            }

            Undo.RegisterCreatedObjectUndo(go.gameObject, "Create " + go.name);
            Selection.activeObject = go.gameObject;
        }
    }
}