using UnityEditor;
using UnityEngine;

namespace USFB
{
    public static class CreateFromHierarchyMenu
    {
        [MenuItem("GameObject/UI/Open File Input", false, 1)]
        private static void CreateInHierarchy(MenuCommand menuCommand) => Create("File Input Field");

        private static void Create(string name)
        {
            var guids = AssetDatabase.FindAssets($"{name} t:Prefab");

            if (guids.Length == 0)
            {
                Debug.LogError($"No prefab '{name}' found in project.");
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
                Debug.LogError($"Failed to instantiate prefab '{name}'.");
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