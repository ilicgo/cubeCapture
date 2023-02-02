using System;

namespace Alkacom.Scripts
{
    [Serializable]
    public sealed class Shape 
    {
       
        public int width;
        public int height;
        public int[] data;
        
        public int Get(int x, int y)
        {
            var index = GetIndex(x, y);
            return data[index];
        }

        public void Set(int x, int y, int val) => data[GetIndex(x, y)] = val;

        private int GetIndex(int x, int y) => (x * height) + y;

        public bool IsOutOfBound(int x, int y) => GetIndex(x, y) >= data.Length;
    }
}