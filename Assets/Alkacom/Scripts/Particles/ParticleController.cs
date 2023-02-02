using System.Collections.Generic;
using System.Linq;
using Alkacom.SDK.CircularPool;
using UnityEngine;

namespace Alkacom.Scripts.Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleController : MonoBehaviour, ICircularPoolItem
    {
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private bool fetchColor;
        private ParticleSystem _ps;
        private Transform _tr;
        private ParticleSystem.MainModule[] _children;

        private void Awake()
        {
            _ps = GetComponent<ParticleSystem>();
            _tr = GetComponent<Transform>();

            if (fetchColor)
                _children = GetComponentsInChildren<ParticleSystem>(true).Select(_ => _.main).ToArray();
            
        }

        public void CircularInit()
        {
            _ps.Clear(true);
            gameObject.SetActive(false);
        }

        public void CircularReset()
        {
            CircularInit();
        }

        public void Place(Vector3 mainWorldPosition)
        {
            _tr.position = mainWorldPosition+positionOffset;
            _ps.Play(true);
            gameObject.SetActive(true);
            
        }

        public ParticleController SetColor(Color color)
        {
            if (!fetchColor) return this;

            for (int i = 0, imax = _children.Length; i < imax; i++)
            {
                _children[i].startColor = color;
            }
            return this;
        }
    }
}