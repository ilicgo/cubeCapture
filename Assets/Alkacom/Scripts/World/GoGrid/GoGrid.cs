namespace Alkacom.Scripts
{
    public sealed class GoGrid : Grid<GoCell>
    {
        public GoGrid(int x, int y, GoCell defaultCell) : base(x, y, defaultCell)
        {
        }
    }
}