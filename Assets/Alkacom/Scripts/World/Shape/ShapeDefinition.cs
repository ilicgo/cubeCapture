using UnityEngine;

namespace Alkacom.Scripts
{
    [CreateAssetMenu(menuName = "Alkacom/Game/ShapeDefinition", fileName = "ShapeDefinition")]
    public sealed class ShapeDefinition : ScriptableObject
    {
        [SerializeField][HideInInspector]  private Shape shape;

        public Shape GetShape() => shape;
    }
}