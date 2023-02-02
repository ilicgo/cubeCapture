using Alkacom.Sdk.Zenject;
using UniRx;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts
{
    public sealed class GoGridBackgroundRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private IFactory<GameObject, GameObject> _factory;
        private SimpleMergeGameObject _simpleMerge;

        [Inject]
        public void Construct(IRegisterSelf<GoGrid> rsGrid, IFactory<GameObject, GameObject> factory)
        {
            _factory = factory;
            _simpleMerge = new SimpleMergeGameObject("background");
            
            rsGrid.NotNullObservable.TakeUntilDestroy(this).Subscribe(StartWitGrid);
        }
        
        void StartWitGrid(GoGrid shapeGrid)
        {
            Clear();
           
            
            foreach (var cell in shapeGrid.IterateAll)
            {
                var go = _factory.Create(prefab);
                go.transform.position = toVec3(cell.Position);
                _simpleMerge.Add(go);
                
            }
            
            _simpleMerge.Merge();
            


        }

        private Vector3 toVec3(Vector2Int cellPosition) => new Vector3(cellPosition.x, 0, cellPosition.y);

        private void Clear()
        {
            _simpleMerge.Destroy();
        }
    }
}