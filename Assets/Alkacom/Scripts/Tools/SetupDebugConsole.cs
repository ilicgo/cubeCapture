using System.Linq;
using Alkacom.Game;
using Alkacom.SDK;
using Alkacom.SDK.Alkacom.SDK.DocumentDB;
using Alkacom.Sdk.Common.States;
using Alkacom.Sdk.Sound;
using Sdk.Common.GameState;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class SetupDebugConsole : MonoBehaviour
    {
        private IDebugConsole _console;
        private IDocumentDB _documentDB;
        private IDuxDispatcher<UIPanelReducer.Action> _uiPanelAction;
        private IGameState _gameState;
        private ILevelState _levelState;
        private ISound _sound;

        [Inject]
        public void Construct(
            IDebugConsole console, 
            IDocumentDB documentDB, 
            IDuxDispatcher<UIPanelReducer.Action> uiPanelAction,
            IGameState gameState,
            ILevelState levelState,
            ISound sound)
        {
            _sound = sound;
            _levelState = levelState;
            _console = console;
            _gameState = gameState;
            
            _documentDB = documentDB;
            
            _uiPanelAction = uiPanelAction;
        }

        private void Start()
        {
            _console.AddButton("RES", () => _documentDB.RemoveAll(), Color.red);
         
            _console.AddButton("UIFail", () =>
            {
                _gameState.SetStatus(GameStateStatus.Pause);
                OpenPanel(UIPanelNameList.Fail);
            }, Color.green);
            
            _console.AddButton("UIWin", () =>
            {
                _gameState.SetStatus(GameStateStatus.Pause);
                OpenPanel(UIPanelNameList.Win);
                _levelState.FlagSuccess();
            }, Color.green);
            
            _console.AddButton("SFX Close Door", () =>
            {
                _sound.GetFx(SoundEnumList.CloseBasket).Play();
            }, Color.yellow);
        
        }

     

       
        private void OpenPanel(UIPanelName panelName)
        {
            _uiPanelAction.Push(UIPanelReducer.ActionCreator.OpenPanel(panelName));
        }
    }
}