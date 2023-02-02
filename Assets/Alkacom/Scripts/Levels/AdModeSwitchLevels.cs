using System;
using Alkacom.Sdk;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class AdModeSwitchLevels : MonoBehaviour, IAdModeEntity
    {
        [SerializeField] private Settings settings;
        [SerializeField] private LevelList ad;
        [SerializeField] private LevelList playing;
        public void SetMode(Sdk.AdMode mode)
        {
            settings.levelList = mode == Sdk.AdMode.Ad ? ad : playing;
            settings.isAdMode = mode == Sdk.AdMode.Ad;
        }

        
    }
}