using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Settings")]
    public class Settings : ScriptableObjectInstaller<Settings>
    {
        public LevelList levelList;

        public bool isAdMode;
       
    }


}