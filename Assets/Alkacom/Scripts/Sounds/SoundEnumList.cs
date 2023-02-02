using Alkacom.Sdk.Sound;

namespace Alkacom.Game
{
    public static class SoundEnumList
    {
        public static readonly SoundEnum CloseBasket = SoundEnum.Create("CloseBasket");
        public static readonly SoundEnum InBasket = SoundEnum.Create("InBasket");
        public static readonly SoundEnum HeadCollision = SoundEnum.Create("HeadCollision");
        public static readonly SoundEnum HeadObstacleCollision = SoundEnum.Create("HeadObstacleCollision");
        public static readonly SoundEnum Wipe = SoundEnum.Create("Wipe");
        public static readonly SoundEnum Win = SoundEnum.Create("Win");
        public static readonly SoundEnum Lose = SoundEnum.Create("Lose");
        public static readonly SoundEnum Multiply = SoundEnum.Create("Multiply");
    }
}