using System.Collections.Generic;
using Alkacom.Game;
using Alkacom.Scripts.Particles;
using Alkacom.SDK;
using Alkacom.SDK.Alkacom.SDK.DocumentDB;
using Alkacom.SDK.Analytics;
using Alkacom.SDK.BasicPool;
using Alkacom.Sdk.Common.Levels;
using Alkacom.Sdk.Common.States;
using Alkacom.Sdk.Sound;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using Sdk.Common;
using Sdk.Common.GameState;
using SimpleSQL;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private SimpleSQLManager sqlManager;
        [SerializeField] private Settings settings;
        [SerializeField] private SoundSettings soundSettings;
        [SerializeField] private GameObject soundGameObject;
        [SerializeField] ParticleSettings particleSettings;
        [SerializeField] private Camera cam; 
        private static List<IClear> sClearList = new List<IClear>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ReloadScriptDomainReset()
        {
            for (int i = 0, imax = sClearList.Count; i < imax; i++)
            {
                sClearList[i].Clear();
            }
            
            sClearList.Clear();
        }
        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;
            
            var forceEnum = UIPanelNameList.Empty;
            var e2 = SoundEnumList.Lose;
            Container.BindInstance(cam);
            Container.BindInstance(settings);
            BindInput();
            BindRegisterSelf();
            BindGameObjectToGameObjectFactory();
            BindPool();
            BindShapeDB();            
            BindGameStatusSimpleState();
          
            BindDocumentDB();
            BindGameStatus();
          
            BindLevelState();
            
            BindLevel();
            
            BindAnalytics();
            BindSound(soundGameObject);
            BindVibration();
            
            CreateParticleGroupPool();
            
            BindDebugConsole();

            CreateControllers();
        }

        private void BindShapeDB()
        {
            var instance = new ShapeDB(settings.shapeDbPrefab, new PrefabToComponentFactory<IShapeRenderer>(Container));
            Container.Bind<IShapeDB>().FromInstance(instance).AsSingle();
        }

        private void BindPool()
        {
            var cellPool = new BasicPool<IGoCellRenderer>(settings.cellPool.size,
                new PrefabToComponentFactory<IGoCellRenderer>(Container));

            Container.Bind<IBasicPool<IGoCellRenderer>>().FromInstance(cellPool).AsSingle();
            
            cellPool.Instantiate(settings.cellPool.prefab);
        }

        private void BindRegisterSelf()
        {
            RegisterSelf<GoGrid>.Bind(Container);
            RegisterSelf<IGrid>.Bind(Container);
         
         
        }


        private void BindLevel()
        {
            var lvlDb = new LevelDB(settings.levelList);
            Container.Bind<ILevelDB<Level>>().FromInstance(lvlDb).AsSingle();

            var loader = Container.Instantiate<LevelLoader>();
        }
        private void BindGameStatusSimpleState()
        {
            var instance = new SimpleState<GameStatusState>(GameStatusState.Waiting);
            Container.Bind<ISimpleState<GameStatusState>>().FromInstance(instance).AsSingle();
            
        }
        private void CreateControllers()
        {
            
            var InputController = Container.Instantiate<InputController>();
            var levelController = Container.Instantiate<LevelController>();
            var GoCellPlacementController = Container.Instantiate<GoCellPlacementController>();
            
        }

        private void BindInput()
        {
           
            var inputSimple = new InputSimpleTouchToObservable();
            Container.Bind<IInputSimpleTouchToObservable>().FromInstance(inputSimple);
            sClearList.Add(inputSimple);
           
        }

        
        private void BindGameStatus()
        {
            var instance = new GameState(GameStateStatus.Playing);
            Container.Bind<IGameState>().To<GameState>().FromInstance(instance).AsSingle();
        }

        private void BindGameObjectToGameObjectFactory()
        {
            var instance = new GameObjectToGameObjectFactory(Container);
            Container.Bind<IFactory<GameObject, GameObject>>().To<GameObjectToGameObjectFactory>()
                .FromInstance(instance).AsSingle();
        }
        
        private void BindLevelState()
        {
            var instance = Container.Instantiate<LevelState>();
            Container.Bind<ILevelState>().To<LevelState>().FromInstance(instance).AsSingle();
        }
        
        private void BindAnalytics()
        {
            
#if UNITY_EDITOR
            var instance = new DebugAnalytics();
#else
            var instance = new DebugAnalytics();
//            var instance = new VoodooTinySauceAnalytics();
#endif
            Container.Bind<IProgression>().FromInstance(instance).AsSingle();
        }

        private void BindSound(GameObject audioSourceGameObject)
        {
          
            var sound = new Sound(soundSettings, audioSourceGameObject);
            Container.Bind<ISound>().To<Sound>().FromInstance(sound).AsSingle();
        }
        private void BindVibration()
        {
            
            var vibration = new Vibration();
            Container.Bind<IVibrationPresets>().To<Vibration>().FromInstance(vibration).AsSingle();
            
        }

        private void CreateParticleGroupPool()
        {
           
            var instance = new ParticleGroupPool(particleSettings,
                new PrefabToComponentFactory<ParticleController>(Container));

            Container.BindInstance(instance).AsSingle();
        }

     


        private void BindDebugConsole()
        {
            var consoleInstance = new DebugConsole(); 
            //consoleInstance.ForceActive(true);
            consoleInstance.RegisterDevice("9FF09421-C5CA-5791-B48F-A6595DF5FB7E");
            consoleInstance.RegisterDevice("9d60da65a377bb11d2b4bcb9cae2a871");
            consoleInstance.RegisterDevice("ee8417de7684d6472eb7848d115b9ab0");
            consoleInstance.RegisterDevice("1a0c82d1541e1aeca404deba6b96b0de");
            consoleInstance.RegisterDevice("7c838906cf85da07a4600d435b407af1");
            consoleInstance.RegisterDevice("ffe0eac6a5abc681ba497874d02d3f6b5a27e177");
            
            
            Container.Bind<IDebugConsole>().To<DebugConsole>().FromInstance(consoleInstance);
        }

      


        private void BindDocumentDB()
        {
            var instance = new DocumentDB(sqlManager, 9151230932882);
            Container.BindInterfacesAndSelfTo<ITickable>().FromInstance(instance);
            Container.Bind<IDocumentDB>().To<DocumentDB>().FromInstance(instance).AsSingle();

        }

    }
}