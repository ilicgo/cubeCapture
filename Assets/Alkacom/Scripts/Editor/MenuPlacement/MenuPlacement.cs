using System;
using UnityEditor;
using UnityEngine;

namespace Alkacom.Game.Editor
{
    public static class MenuPlacement
    {
        #region UTILS
        private const int MenuPriority = -50;

        private const string PrefabManagerPath = "Assets/Alkacom/Scripts/Editor/MenuPlacement/MenuPlacementProfileData.asset";

        private static MenuPlacementProfile LocatePrefabManager()=> AssetDatabase.LoadAssetAtPath<MenuPlacementProfile>(PrefabManagerPath);
        
        private static void SafeInstantiate(Func<MenuPlacementProfile, GameObject> itemSelector)
        {
            var prefabManager = LocatePrefabManager();

            if (!prefabManager)
            {
                Debug.LogWarning($"PrefabManager not found at path {PrefabManagerPath}");
                return;
            }

            var item = itemSelector(prefabManager);
            var instance = PrefabUtility.InstantiatePrefab(item, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
        

        #endregion
        
        
        [MenuItem("GameObject/AkGame/helloWorld", priority = MenuPriority)]
        private static void CreatePieceDot() => SafeInstantiate(prefabManager => prefabManager.helloWorld);
   
    }
   
}