using System;
using Cinemachine;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

namespace Alkacom.Scripts
{
    public class CameraTarget : MonoBehaviour, ILevelSyncUpdate, ICameraTarget
    {
        [SerializeField] private Vector3 followOffset;
        [SerializeField] private float zoom;

        private Transform _tr;
        private CinemachineVirtualCamera _cam;
        private CinemachineTransposer _transpose;
        private bool _done;

        private void Start() => PlaceCamera();

        public void LevelSyncUpdate() => PlaceCamera();

        void CacheComponent()
        {
            if(_cam != null) return;
            
            _tr = GetComponent<Transform>();
            _cam = _tr.parent.GetComponentInChildren<CinemachineVirtualCamera>();
            
            _transpose = _cam.GetCinemachineComponent<CinemachineTransposer>();
            
        }
        void PlaceCamera()
        {
            if(_done) return;
            CacheComponent();
            
            _transpose.m_FollowOffset = followOffset;
            
            _cam.m_Follow = _tr;
            _cam.m_LookAt = _tr;
            _cam.m_Lens.OrthographicSize = zoom+3.0f;
        }

        public void ChangeOrientation(float rotation, float changeZoom, Vector2Int offset)
        {
            CacheComponent();
            PlaceCamera();
            _done = true;
            _cam.m_Lens.Dutch = rotation;
            _cam.m_Lens.OrthographicSize =  zoom +3.0f+ changeZoom;
            _transpose.m_FollowOffset = new Vector3(followOffset.x + offset.x, followOffset.y, followOffset.z + offset.y);
        }

        
    }
}