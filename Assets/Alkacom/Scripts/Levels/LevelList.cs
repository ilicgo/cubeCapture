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
        [SerializeField] Level[] levels;
        [SerializeField] Level[] levelsLoop;
        public Level Get(int number)
        {
            if (levels.Length > number - 1 || levelsLoop.Length == 0)
                return levels[(number - 1) % levels.Length];

            return levelsLoop[(number - 1) % levelsLoop.Length];

        }
        
    }
}