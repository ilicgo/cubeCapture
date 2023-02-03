using System.Linq;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;

namespace Alkacom.Scripts
{
    public sealed class CheckFailConditionController
    {
        private GoGrid _grid;
        private readonly ISimpleState<GameStatusState> _ssGss;
        private readonly IShapeDB _shapeDB;
        private readonly IPutOnGridController _putOnGridController;

        public CheckFailConditionController(IPutOnGridController putOnGridController, IRegisterSelf<GoGrid> rsGrid, ISimpleState<ShapePlacementMessage> ssMessage,ISimpleState<GameStatusState> ssGss, IShapeDB shapeDB)
        {
            _putOnGridController = putOnGridController;
            _shapeDB = shapeDB;
            _ssGss = ssGss;
            rsGrid.NotNullObservable.Subscribe(_ => _grid = _);
            ssMessage.Observable.Where(_ => _ == ShapePlacementMessage.Add && _grid != null && ssGss.CurrentState != GameStatusState.Fail).Subscribe(UpdateGrid);
        }

        void UpdateGrid(ShapePlacementMessage message)
        {
            var emptySpace = _grid.IterateAll.Where(_ => _.Data == GoCell.Empty).ToArray();
            var shapes = _shapeDB.GetActiveShapes().Select(_ => _.GetShape()).ToArray();


            for (int j = 0, jMax = shapes.Length; j < jMax; j++)
            {
                for (int i = 0, imax = emptySpace.Length; i < imax; i++)
                {
                    if (_putOnGridController.HaveSpace(shapes[j], emptySpace[i].Position))
                    {
                        return;
                    }
                }
            }
            
            _ssGss.Set(GameStatusState.Fail);
               
        }

    
    }
}