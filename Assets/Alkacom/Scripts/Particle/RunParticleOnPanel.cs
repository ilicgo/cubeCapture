using System;
using Alkacom.SDK;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class RunParticleOnPanel : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private string panelName;
        [SerializeField] private float delay = 1.0f;
        private UIPanelName _panelName;

        [Inject]
        public void Construct(IDuxState<UIPanelReducer.State> panelState)
        {
            _panelName = UIPanelName.FindByName(panelName);
            
            panelState
                .StateObservable()
                .TakeUntilDestroy(this)
                .Where(_ => _panelName == _.PanelName)
                .Delay(TimeSpan.FromSeconds(delay))
                .Subscribe(_ => TriggerParticle());
        }

        private void TriggerParticle()
        {
            for(int i = 0, imax = particles.Length; i <imax; i++)
                particles[i].Play(true);
        }
    }
}