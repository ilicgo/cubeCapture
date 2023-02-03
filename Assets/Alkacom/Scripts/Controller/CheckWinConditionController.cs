using System.Linq;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class CheckWinConditionController
    {
        private GoGrid _grid;
        private readonly ISimpleState<GameStatusState> _ssGss;

        public CheckWinConditionController(IRegisterSelf<GoGrid> rsGrid, ISimpleState<ShapePlacementMessage> ssMessage,ISimpleState<GameStatusState> ssGss)
        {
            _ssGss = ssGss;
            rsGrid.NotNullObservable.Subscribe(_ => _grid = _);
            ssMessage.Observable.Where(_ => _ == ShapePlacementMessage.Add && _grid != null && ssGss.CurrentState != GameStatusState.Win).Subscribe(UpdateGrid);
        }

        void UpdateGrid(ShapePlacementMessage message)
        {
            var isWin = _grid.IterateAll.All(_ => _.Data != GoCell.Diamond);
            if (isWin)
                _ssGss.Set(GameStatusState.Win);
        }
    }
}
