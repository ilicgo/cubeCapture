using System;
using Alkacom.SDK;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public class OnKeyPressOpenPanel : MonoBehaviour
    {
        [SerializeField] private string panelName;
        private bool _isAd;
        
        private UIPanelName _panel;
        [SerializeField] private KeyCode triggerKey;
        private IDuxDispatcher<UIPanelReducer.Action> _panelDispatcher;

        [Inject]
        public void Construct(Settings settings, IDuxDispatcher<UIPanelReducer.Action> panelDispatcher)
        {
            _isAd = settings.isAdMode;
            _panelDispatcher = panelDispatcher;
            _panel = UIPanelName.FindByName(panelName);
            
        }

        private void Update()
        {
            if(!_isAd) return;

            if (Input.GetKey(triggerKey))
                _panelDispatcher.Push(UIPanelReducer.ActionCreator.OpenPanel(_panel));
            
        }
    }
}