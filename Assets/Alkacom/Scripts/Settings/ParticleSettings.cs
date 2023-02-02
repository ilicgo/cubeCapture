using System;
using UnityEngine;

namespace Alkacom.Scripts.Particles
{
    [CreateAssetMenu(menuName = "Alkacom/ParticleSettings")]
    public class ParticleSettings : ScriptableObject
    {
        [Serializable]
        public struct Group
        {
            public ParticleId particleId;
            public GameObject prefab;
            public int size;
        }

        public Group[] groups;
    }
}

