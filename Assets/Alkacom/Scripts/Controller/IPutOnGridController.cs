using UnityEngine;

namespace Alkacom.Scripts
{
    public interface IPutOnGridController
    {
        public bool TryToPlace(Shape shape, Vector3 hitPoint);
        public bool HaveSpace(Shape shape, Vector2Int hitPoint);
    }
}