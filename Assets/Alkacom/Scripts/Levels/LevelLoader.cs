using System.Linq;
using Alkacom.Sdk.Common.Levels;
using Alkacom.Sdk.Common.States;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class LevelLoader
    {
        private readonly ILevelDB<Level> _db;
        private readonly IFactory<GameObject, GameObject> _facotry;
        private GameObject _instance;
        private readonly ISimpleState<GameStatusState> _gameStatusSimpleState;
        private GameObject _loadedLevel;
        private readonly IRegisterSelf<GoGrid> _rsGoGrid;
        private readonly IRegisterSelf<IGrid> _rsGridGeneric;
        private readonly IShapeDB _shapeDb;


        public LevelLoader(IShapeDB shapeDB, IRegisterSelf<GoGrid> rsGoGrid,IRegisterSelf<IGrid> rsGridGeneric, ISimpleState<GameStatusState> gameStatusSimpleState, ILevelState levelState, ILevelDB<Level> db, IFactory<GameObject, GameObject> factory)
        {
            _shapeDb = shapeDB;
            _rsGridGeneric = rsGridGeneric;
            _rsGoGrid = rsGoGrid;
            _gameStatusSimpleState = gameStatusSimpleState;
            _facotry = factory;
            _db = db;
            
            
            levelState.Observable().Subscribe(LoadLevel);
        }

        void LoadLevel(int number)
        {
            
            _gameStatusSimpleState.Set(GameStatusState.Loading);

            DestroyCurrentLevel();
            LoadNewLevel(number);
            
            _gameStatusSimpleState.Set(GameStatusState.Ready);
        }

        private void LoadNewLevel(int number)
        {
            var level = _db.Get(number);
            var prefab = level.GetPrefab(number);
            _loadedLevel = _facotry.Create(prefab);

            _shapeDb.Build(level.GetShapeDBDefinition(number));
            
            var grid = new GoGrid(6, 6, GoCell.Empty);
            
            grid.Put(new Vector2Int(4,4), GoCell.Diamond);
            grid.Put(new Vector2Int(2,2), GoCell.Diamond);
            
           
     
            
            _rsGoGrid.Register(grid);
            _rsGridGeneric.Register(grid);
        }

        private void DestroyCurrentLevel()
        {
            if(_loadedLevel == null) return;
            Object.Destroy(_loadedLevel);
            _loadedLevel = null;
           
        }
    }
}