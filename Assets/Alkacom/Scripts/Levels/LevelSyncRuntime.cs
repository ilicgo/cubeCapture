using System;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    [ExecuteInEditMode]
    public sealed class LevelSyncRuntime : MonoBehaviour
    {
        [SerializeField] private int slowUpdateFrame = 240;
        private bool _isConstruct;
        private int _slowUpdateFrameCount;
        [Inject]
        public void Construct()
        {
            _isConstruct = true;
            _slowUpdateFrameCount = 0;

        }

        private void Start()
        {
            if(!_isConstruct) return;
            var items = GetComponentsInChildren<ILevelStart>();
            RunStart(LevelStartStatus.BuildGrid, items);
            RunStart(LevelStartStatus.PlaceRail, items);
        }
        
        private void RunStart(LevelStartStatus status, ILevelStart[] items )
        {
            for(int i = 0, imax = items.Length; i < imax; i++)
                items[i].LevelStart(status);
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if(_isConstruct) return;
            _slowUpdateFrameCount++;
            if (_slowUpdateFrameCount > slowUpdateFrame)
            {
                _slowUpdateFrameCount = 0;
                UpdateSyncSlowUpdate();
            }

            UpdateSyncUpdate();
        }

        private void UpdateSyncUpdate()
        {
            var items = GetComponentsInChildren<ILevelSyncUpdate>();
            
            for(int i = 0, imax = items.Length; i < imax; i++)
                items[i].LevelSyncUpdate();
        }
        
        private void UpdateSyncSlowUpdate()
        {
            var items = GetComponentsInChildren<ILevelSyncSlowUpdate>();
            
            for(int i = 0, imax = items.Length; i < imax; i++)
                items[i].LevelSyncSlowUpdate();
        }
        #endif
    }
}