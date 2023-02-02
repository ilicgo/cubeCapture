using System.Linq;
using Alkacom.SDK;
using UnityEngine;
using Random = System.Random;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Game/Level")]
    public sealed class Level : ScriptableObject
    {
       
        [SerializeField] private GameObject prefab;

        
        public GameObject GetPrefab(int number) => prefab;

    
    }
}