using UnityEngine;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Game/Level")]
    public sealed class LevelHandmade : Level
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ShapeDBDefinition shapeDB;
        public override GameObject GetPrefab(int number) => prefab;
        public override ShapeDBDefinition GetShapeDBDefinition(int number) => shapeDB;
    }
}