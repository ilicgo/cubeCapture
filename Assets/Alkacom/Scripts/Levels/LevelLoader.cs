using System.Linq;
using Alkacom.Sdk.Common.Levels;
using Alkacom.Sdk.Common.States;
using Alkacom.Sdk.State;
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



        public LevelLoader(ISimpleState<GameStatusState> gameStatusSimpleState, ILevelState levelState, ILevelDB<Level> db, IFactory<GameObject, GameObject> factory)
        {

         
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
            
           
        }

        private void DestroyCurrentLevel()
        {
            if(_loadedLevel == null) return;
            Object.Destroy(_loadedLevel);
            _loadedLevel = null;
           
        }
    }
}