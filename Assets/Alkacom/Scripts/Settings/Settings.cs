using System;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Settings")]
    public class Settings : ScriptableObjectInstaller<Settings>
    {
        public LevelList levelList;

        public bool isAdMode;

        public PoolSettings cellPool;
        public GameObject shapeDbPrefab;
    }

    [Serializable]
    public struct PoolSettings
    {
        public GameObject prefab;
        public int size;
    }

}