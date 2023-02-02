using System;

namespace Alkacom.Scripts
{
    public interface IShapeRenderer
    {
        void DestroySelf();
        void Build(ShapeDefinition def);
        bool IsFree { get;  }
      
        void Place(IPositionTr position);
    }
}