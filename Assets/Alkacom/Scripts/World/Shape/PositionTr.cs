using System;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class PositionTr : MonoBehaviour, IPositionTr
    {
        private Transform _tr;
        public bool IsFree { get; private set; }
        public Vector3 Position => _tr.position;

        private void Awake()
        {
            IsFree = true;
            _tr = GetComponent<Transform>();
        }

        public void TakeOwnership()
        {
            IsFree = false;
        }

        public void LeaveOwnership()
        {
            IsFree = true;
        }
    }
}