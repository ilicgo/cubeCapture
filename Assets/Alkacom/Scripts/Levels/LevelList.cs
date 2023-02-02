using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Game/LevelList")]
    public sealed class LevelList : ScriptableObject
    {
       
        
        public Level[] levels;
        
        public Level Get(int number) => levels[(number-1) % levels.Length];
        
    }
}