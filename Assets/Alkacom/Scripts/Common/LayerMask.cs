using UnityEngine;

namespace Alkacom.Scripts
{
    public static class LayerMask
    {
        public static int Pickable { get; } = UnityEngine.LayerMask.GetMask("Pickable");
        public static int Ground { get; } = UnityEngine.LayerMask.GetMask("Ground");
    }
}