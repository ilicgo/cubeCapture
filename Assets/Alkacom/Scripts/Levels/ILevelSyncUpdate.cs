namespace Alkacom.Scripts
{
    public interface ILevelSyncUpdate
    {
        void LevelSyncUpdate();
    }
    
    public interface ILevelSyncSlowUpdate
    {
        void LevelSyncSlowUpdate();
    }

    public enum LevelStartStatus
    {
        BuildGrid,
        PlaceRail
    }
    public interface ILevelStart
    {
        void LevelStart(LevelStartStatus levelStartStatus);
    }
}