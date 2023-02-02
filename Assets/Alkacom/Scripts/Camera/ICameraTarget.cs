using UnityEngine;

namespace Alkacom.Scripts
{
    public interface ICameraTarget
    {
        public void ChangeOrientation(float rotation, float changeZoom, Vector2Int offset);
    }
}