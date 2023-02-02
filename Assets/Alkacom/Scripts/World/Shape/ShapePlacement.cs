using System;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public sealed class ShapePlacement : MonoBehaviour
    {
        private IShapeDB _shapeDB;
        private IPositionTr[] _positions;

        [Inject]
        public void Construct(IShapeDB shapeDB)
        {
            _shapeDB = shapeDB;
            
            _positions = GetComponentsInChildren<IPositionTr>();
            Observable.Timer(TimeSpan.FromSeconds(1.5f)).TakeUntilDestroy(this).Subscribe(RxUpdate);

        }



        private void RxUpdate(long l)
        {
            for (int i = 0, imax = _positions.Length; i < imax; i++)
            {
                if (_positions[i].IsFree)
                {
                    Place(_positions[i]);
                }
            }
        }

        private void Place(IPositionTr position)
        {
            var optShape = _shapeDB.Get();
            if(optShape.IsEmpty) return;

            var shape = optShape.Value;

            shape.Place(position);
        }

       
    }
}