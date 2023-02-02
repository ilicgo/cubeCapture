using Alkacom.SDK.BasicPool;
using Alkacom.Sdk.Zenject;
using UniRx;

namespace Alkacom.Scripts
{
    public sealed class GoCellPlacementController
    {
        private readonly IBasicPool<IGoCellRenderer> _poolCells;

        public GoCellPlacementController(IRegisterSelf<GoGrid> rsGrid, IBasicPool<IGoCellRenderer> poolCells)
        {
            _poolCells = poolCells;
            rsGrid.NotNullObservable.Subscribe(StartWithGrid);
        }

        void StartWithGrid(GoGrid grid)
        {

            _poolCells.RecycleAll();
            
            foreach (var cell in grid.IterateAll)
                _poolCells.Get().Place(cell);
        }

       
    }
}