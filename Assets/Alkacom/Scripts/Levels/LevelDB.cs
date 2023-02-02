using Alkacom.Scripts;
using Alkacom.Sdk.Common.Levels;
using Alkacom.Sdk.Tools;
using UnityEngine;

namespace Alkacom.Game
{
    public class LevelDB : ILevelDB<Level>
    {
        private readonly LevelList _levelsList;

        public LevelDB(LevelList levelsList)
        {
            _levelsList = levelsList;
        }

        public Level Get(int levelIndex) => _levelsList.Get(levelIndex);
        
    }
}