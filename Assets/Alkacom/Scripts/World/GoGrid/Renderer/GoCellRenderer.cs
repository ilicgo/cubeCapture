using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class GoCellRenderer : MonoBehaviour, IGoCellRenderer
    {
        [SerializeField] private Transform trDiamond;
        [SerializeField] private Transform trCube;
        
        
        private IGridData<GoCell> _cell;
        private Transform _tr;

        public void PoolInit()
        {
            _tr = GetComponent<Transform>();
            Recycle();
        }

        public void Recycle()
        {
            RxUpdate(GoCell.Empty);
            gameObject.SetActive(false);
            
        }

        public bool IsRecycled() => !gameObject.activeSelf;
        public void Place(IGridData<GoCell> cell)
        {
            _cell = cell;
            _tr.position = ToVec3(cell.Position);
            RxUpdate(cell.Data);
            gameObject.SetActive(true);
            cell.Observable.TakeUntilDisable(this).Subscribe(RxUpdate);
        }

        private Vector3 ToVec3(Vector2Int cellPosition) => new Vector3(cellPosition.x, 0, cellPosition.y);

        private void RxUpdate(GoCell cell)
        {
            trDiamond.gameObject.SetActive(cell == GoCell.Diamond);
            trCube.gameObject.SetActive(cell == GoCell.Cube);
            
        }
    }
}