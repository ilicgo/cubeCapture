using Alkacom.SDK;
using Alkacom.Sdk.Common.States;
using Sdk.Common.GameState;
using Zenject;

namespace Alkacom.Scripts
{
    public class ButtonDispatcherRestart : UIButton
    {
       
        private IDuxDispatcher<UIPanelReducer.Action> _panelAction;
        private IGameState _gameState;
        private ILevelState _levelState;


        [Inject]
        public void Construct(
            IDuxDispatcher<UIPanelReducer.Action> panelAction,
            ILevelState levelState,
            IGameState gameState)
        {
            _levelState = levelState;
            _gameState= gameState;
            _panelAction = panelAction;
        }
        public override void OnClick()
        {
            _levelState.Restart();
            _gameState.SetStatus(GameStateStatus.Playing);
            _panelAction.Push(UIPanelReducer.ActionCreator.OpenPanel(UIPanelNameList.Hud));
        }
    }
}