using UnityEngine;

namespace Alkacom.Scripts
{
    public interface IPickable
    {
        void Pick();
        void UnPick();
        void Move(Vector3 hitInfoPoint);
    }
}