using Alkacom.SDK;
using Alkacom.Sdk.State;
using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class InputController
    {
        private readonly Camera _camera;
        private IPickable _pickable;

        public InputController(ISimpleState<GameStatusState> ssGameStatus, IInputSimpleTouchToObservable input, Camera mainCamera)
        {
            
            _camera = mainCamera;
            var obs = input
                .GetObservable()
                .Where(_ =>  ssGameStatus.CurrentState == GameStatusState.Ready);
                
                obs.Where(_ => _.Phase == AkTouchPhase.Start).Subscribe(OnTouchStart);
                obs.Where(_ => _.Phase == AkTouchPhase.Move && _pickable != null).Subscribe(OnTouchMove);
                obs.Where(_ => _.Phase == AkTouchPhase.End).Subscribe(OnTouchEnd);

          
        }
        
        void OnTouchStart(AkTouchState state)
        {
            var ray = _camera.ScreenPointToRay(state.Position);
            
            if (!Physics.Raycast(ray, out var hit, float.MaxValue, LayerMask.Pickable)) return;

            _pickable = hit.collider.GetComponent<IPickable>();
            _pickable.Pick();
            
        }
        
        void OnTouchMove(AkTouchState state)
        {
            var ray = _camera.ScreenPointToRay(state.Position);
            var mask = LayerMask.Ground;
            if (!Physics.Raycast(ray, out var hit, float.MaxValue, mask)) return;
           
            _pickable.Move(hit.point);
        }
        
        void OnTouchEnd(AkTouchState state)
        {
            _pickable?.UnPick();
            _pickable = null;
        }
        
    }
}