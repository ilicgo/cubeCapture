using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public sealed class ShapeRenderer : MonoBehaviour, IShapeRenderer, IPickable
    {
        [SerializeField] private GameObject prefab;
        private IFactory<GameObject, GameObject> _factory;
        private SimpleMergeGameObject _goMerge;
        private Transform _tr;
        private IPositionTr _positionTr;
        private Shape _shape;
        private IPutOnGridController _putOnGridController;
        
        [Inject]
        public void Construct(IFactory<GameObject, GameObject> factory, IPutOnGridController putOnGridController)
        {
            _putOnGridController = putOnGridController;
            _tr = GetComponent<Transform>();
            _factory = factory;
            _goMerge = new SimpleMergeGameObject("Shape", GetComponent<Transform>());
           
        }
        private void Awake()
        {
            IsFree = true;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public void Build(ShapeDefinition def)
        { 
            _shape = def.GetShape();
            
            
            for(int ix = 0, ixMax = _shape.width; ix <ixMax; ix++)
            for (int iy = 0, iyMax = _shape.height; iy < iyMax; iy++)
                CreateChunk(_goMerge, ix,iy,_shape.Get(ix, iy) == 1);
            
            _goMerge.Merge();

            var root = _goMerge.GetRoot();
            var tempBoxCollider = root.AddComponent<BoxCollider>();
           

            var t = gameObject.AddComponent<BoxCollider>();
            t.center = tempBoxCollider.center;
            t.size = tempBoxCollider.size;
            t.isTrigger = true;
            
            Destroy(tempBoxCollider);
            
            gameObject.SetActive(false);
        }

        private void CreateChunk(SimpleMergeGameObject go, int x, int y, bool b)
        {
            if(!b) return;

            var instance = _factory.Create(prefab);
            var instanceTr = instance.GetComponent<Transform>();
            instanceTr.position = new Vector3(x, 0, y);
            
            go.Add(instance);
        }

        public bool IsFree { get; private set; }

       

        public void Place(IPositionTr position)
        {
            IsFree = false;
            _positionTr = position;
            _positionTr.TakeOwnership();
            _tr.position = _positionTr.Position;
            _tr.localScale = Vector3.one * 0.75f;
            gameObject.SetActive(true);
        }

        public Shape GetShape() => _shape;

        private void OnDestroy()
        {
            _goMerge.Destroy();
        }

        public void Pick()
        {
            _tr.localScale = Vector3.one ;
        }

        public void UnPick()
        {
            
            if (Physics.Raycast(_tr.position, Vector3.down, out var hit, float.MaxValue, LayerMask.Ground))
            {
                if (_putOnGridController.TryToPlace(_shape, hit.point))
                {
                    _positionTr.LeaveOwnership();
                    IsFree = true;
                    gameObject.SetActive(false);
                    return;
                }
            }
            
            _tr.localScale = Vector3.one * 0.75f;
            _tr.position = _positionTr.Position;
        }

      

        public void Move(Vector3 hitInfoPoint)
        {
            _tr.position = hitInfoPoint + Vector3.up;
        }

      

       
       
    }
}