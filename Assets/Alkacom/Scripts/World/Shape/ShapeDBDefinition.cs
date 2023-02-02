using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Game/ShapeDB", fileName = "ShapeDB")]
    public sealed class ShapeDBDefinition : ScriptableObject
    {
        [SerializeField] private ShapeDefinition[] shapes;
        public IEnumerable<ShapeDefinition> Iterator => shapes;
    }
}