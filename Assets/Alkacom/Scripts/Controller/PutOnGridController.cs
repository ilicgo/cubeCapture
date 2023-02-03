using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public class PutOnGridController : IPutOnGridController
    {
        private GoGrid _grid;
        private readonly ISimpleState<ShapePlacementMessage> _ssShapeAdd;

        public PutOnGridController(IRegisterSelf<GoGrid> rsGrid, ISimpleState<ShapePlacementMessage> ssShapeAdd)
        {
            _ssShapeAdd = ssShapeAdd;
            rsGrid.NotNullObservable.Subscribe(_ => _grid = _);
        }

        public bool HaveSpace(Shape shape, Vector2Int gridPosition)
        {
            for (int ix = 0, ixMax = shape.width; ix < ixMax; ix++)
            {
                for (int iy = 0, iyMax = shape.height; iy < iyMax; iy++)
                {
                    if(shape.Get(ix,iy) != 1) continue;
                    var pos = new Vector2Int(ix+gridPosition.x, iy+gridPosition.y);
                    if(_grid.IsOutBound(pos)) return false;
                    if (_grid.Get(pos).Data != GoCell.Empty) return false;
                }
            }

            return true;
        }
        public bool TryToPlace(Shape shape, Vector3 hitPoint)
        {
            if (_grid == null) return false;
            
            var gridPosition = new Vector2Int(Mathf.RoundToInt(hitPoint.x), Mathf.RoundToInt(hitPoint.z));

            if (!HaveSpace( shape, gridPosition)) return false;
            
            for (int ix = 0, ixMax = shape.width; ix < ixMax; ix++)
            {
                for (int iy = 0, iyMax = shape.height; iy < iyMax; iy++)
                {
                    if(shape.Get(ix,iy) != 1) continue;
                    var pos = new Vector2Int(ix+gridPosition.x, iy+gridPosition.y);
                   
                    _grid.Put(pos, GoCell.Cube);
                }
            }

            _ssShapeAdd.Set(ShapePlacementMessage.Add);
            _ssShapeAdd.Set(ShapePlacementMessage.None);
            return true;
        }
    }
}