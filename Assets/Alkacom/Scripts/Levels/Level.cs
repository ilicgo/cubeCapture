using System.Linq;
using Alkacom.SDK;
using UnityEngine;
using Random = System.Random;

namespace Alkacom.Scripts
{
    
    public abstract class Level : ScriptableObject
    {
        public abstract GameObject GetPrefab(int number);
        public abstract ShapeDBDefinition GetShapeDBDefinition(int number);
    }
}


