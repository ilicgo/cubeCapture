using System;
using Alkacom.SDK;
using Alkacom.SDK.Analytics;
using Alkacom.Sdk.Common.States;
using Alkacom.Sdk.State;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public class LevelController
    {
        private readonly ISimpleState<GameStatusState> _gameStatusSimpleState;
       
        private readonly ILevelState _levelState;
        private readonly IDuxDispatcher<UIPanelReducer.Action> _panelDispatch;
        private readonly IProgression _progression;


        public LevelController(
            ISimpleState<GameStatusState> gameStatusSimpleState,
         
            ILevelState levelState,
            IProgression progression,
            IDuxDispatcher<UIPanelReducer.Action> panelDispatch
        )
        {
            _panelDispatch = panelDispatch;
            _progression = progression;
            _levelState = levelState;
            
           
            _gameStatusSimpleState = gameStatusSimpleState;

            
            _gameStatusSimpleState.Observable.Where(_ => _ == GameStatusState.Loading).Subscribe(OnLoading);
            _gameStatusSimpleState.Observable.Where(_ => _ == GameStatusState.Win).Subscribe(OnWin);
           
            _progression.SendProgression(ProgressionStatus.Start, _levelState.CurrentLevel.ToString());
           
        }

        void OnLoading(GameStatusState gss)
        {
        
            _progression.SendProgression(ProgressionStatus.Start, _levelState.CurrentLevel.ToString());
        }
        
        void OnWin(GameStatusState gss)
        {
           
           
            
            _progression.SendProgression(ProgressionStatus.Win, _levelState.CurrentLevel.ToString());
            _levelState.FlagSuccess();
            _panelDispatch.Push(UIPanelReducer.ActionCreator.OpenPanel(UIPanelNameList.Win));

        }
        

       
       
    }
}