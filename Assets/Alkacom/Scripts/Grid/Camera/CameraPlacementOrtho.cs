using Alkacom.Sdk.Zenject;
using Cinemachine;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class CameraPlacementOrtho : MonoBehaviour
    {
        private Transform _tr;
        private CinemachineVirtualCamera _vCam;
        private Settings _settings;
        private IGrid _grid;
        [SerializeField] private Vector2 positionOffset;
        [SerializeField] private Vector2 positionMult;
        [SerializeField] private Vector2 orthMult;
        [SerializeField] private Vector2 orthSizeRang;
        [SerializeField] private float startZoom;
        [SerializeField] private float adChangeZoom;
        

        [Inject]
        public void Construct(IRegisterSelf<IGrid> rsGrid, Settings settings)
        {
            _tr = GetComponent<Transform>();
            _vCam = GetComponent<CinemachineVirtualCamera>();
           
            
            _settings = settings;
            
            rsGrid.NotNullObservable.TakeUntilDestroy(this).Subscribe(MoveCamera);
        }

        void MoveCamera(IGrid grid)
        {
            _grid = grid;

            var width = _grid.Width;
            var height = _grid.Height;

            var withPos = (1 + width)* positionMult.x + positionOffset.x;
            var heightPos = (1 + height) * positionMult.y + positionOffset.y;
            
            var center = new Vector3(withPos, 0, heightPos);

            _tr.position = center + _tr.forward * -25;

       
            var orthW = width * orthMult.x;
            var orthH = height * orthMult.y;

            var orth = Mathf.Max(orthW, orthH) + startZoom;

            if (_settings.isAdMode)
            {
                orth += adChangeZoom;
            }
            if (orth > orthSizeRang.y)
                orth = orthSizeRang.y;
            if (orth < orthSizeRang.x)
                orth = orthSizeRang.x;

            _vCam.m_Lens.OrthographicSize = orth;
        }
        
        [Button]
        void ButtonSync() => MoveCamera(_grid);
    }
}