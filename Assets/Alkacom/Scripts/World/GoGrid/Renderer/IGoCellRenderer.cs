using Alkacom.SDK.BasicPool;
using UnityEngine;

namespace Alkacom.Scripts
{
    public interface IGoCellRenderer :  IBasicPoolItem
    {
        void Place(IGridData<GoCell> cell);
    }
}