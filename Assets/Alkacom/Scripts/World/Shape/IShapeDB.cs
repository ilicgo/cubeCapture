using Alkacom.SDK;

namespace Alkacom.Scripts
{
    public interface IShapeDB
    {
        public void Reset();
        public void Build(ShapeDBDefinition data);
        public Option<IShapeRenderer> Get();

        IShapeRenderer[] GetActiveShapes();
    }
}