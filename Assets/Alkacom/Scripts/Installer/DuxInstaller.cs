using System;
using Alkacom.SDK;
using UniRx;
using Zenject;

namespace Alkacom.Scripts
{
    public class DuxInstaller : MonoInstaller<DuxInstaller>
    {
        private DuxStoreManager _duxStoreManager;

        public override void InstallBindings()
        {
            _duxStoreManager = new DuxStoreManager(new IDuxStoreUpdate[]
                {BuildPanel(), BuildGameState()});

            Container.BindInterfacesAndSelfTo<DuxStoreManager>().FromInstance(_duxStoreManager);
        }

        private IDuxStoreUpdate BuildGameState()
        {
            var store = GameStatusReducerBindHelper.CreateStore(new GameStatusReducer.State(false));
            GameStatusReducerBindHelper.Bind(Container, store);
            //@todo tmp fix use level stats to kwno when level is loading or not
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ => store.Push(GameStatusReducer.ActionCreator.SetIsRunning(true)));
            return store;
        }

        private IDuxStoreUpdate BuildPanel()
        {
            var panelStore = UIPanelReducerBindHelper.CreateStore(new UIPanelReducer.State(UIPanelNameList.Hud));
            UIPanelReducerBindHelper.Bind(Container, panelStore);
            return panelStore;
        }

        
    }
}