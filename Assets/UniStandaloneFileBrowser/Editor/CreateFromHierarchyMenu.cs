using UnityEditor;
using UnityEngine;

namespace USFB
{
    public static class CreateFromHierarchyMenu
    {
        [MenuItem("GameObject/UI/Open File Input", false, 1)]
        private static void CreateInHierarchy(MenuCommand menuCommand)
        {
            var guids = AssetDatabase.FindAssets("File Input Field t:Prefab");

            if (guids.Length == 0)
            {
                Debug.LogError("No prefab 'File Input Field' found in project.");
                return;
            }

            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            var prefab = AssetDatabase.LoadAssetAtPath<OpenFileInput>(assetPath);
            var parent = Selection.activeGameObject;

            if (!parent || !parent.TryGetComponent<Canvas>(out _))
            {
                parent = Object.FindFirstObjectByType<Canvas>().gameObject;
            }

            var go = PrefabUtility.InstantiatePrefab(prefab) as OpenFileInput;

            if (!go)
            {
                Debug.LogError("Failed to instantiate prefab 'File Input Field'.");
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