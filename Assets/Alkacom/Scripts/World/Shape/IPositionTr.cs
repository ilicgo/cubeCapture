using UnityEngine;

namespace Alkacom.Scripts
{
    public interface IPositionTr
    {
        bool IsFree { get;  }
        Vector3 Position { get; }
        void TakeOwnership();
    }
}