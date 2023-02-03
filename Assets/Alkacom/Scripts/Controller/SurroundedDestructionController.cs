using System.Linq;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class SurroundedDestructionController
    {
        private readonly Vector2Int[] surroundedOffset = new[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            
            new Vector2Int(-1, 1),
            new Vector2Int(1, -1),
        };

        
        private GoGrid _grid;

        public SurroundedDestructionController(IRegisterSelf<GoGrid> rsGrid, ISimpleState<ShapePlacementMessage> ssMessage)
        {
            rsGrid.NotNullObservable.Subscribe(_ => _grid = _);
            ssMessage.Observable.Where(_ => _ == ShapePlacementMessage.Add && _grid != null).Subscribe(UpdateGrid);
        }

        void UpdateGrid(ShapePlacementMessage message)
        {
            foreach (var diamond in _grid.IterateAll.Where(_ => _.Data == GoCell.Diamond))
                if (IsSurrounded(diamond))
                    Destroy(diamond);
                
        }

        private void Destroy(IGridData<GoCell> diamond)
        {
            var pos = diamond.Position;

            for (int i = 0, imax = surroundedOffset.Length; i < imax; i++)
            {
                var surroundedPos = surroundedOffset[i] + pos;
                _grid.Put(surroundedPos, GoCell.Empty);
            }
            
            _grid.Put(pos, GoCell.Empty);
        }

       
        private bool IsSurrounded(IGridData<GoCell> diamond)
        {
            var pos = diamond.Position;

            for (int i = 0, imax = surroundedOffset.Length; i < imax; i++)
            {
                var surroundedPos = surroundedOffset[i] + pos;
                if (_grid.IsOutBound(surroundedPos) || _grid.Get(surroundedPos).Data == GoCell.Empty) return false;
            }

            return true;
        }
    }
}
