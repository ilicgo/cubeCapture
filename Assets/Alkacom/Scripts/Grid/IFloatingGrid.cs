using System;
using System.Collections.Generic;
using Alkacom.SDK;
using I2.Loc;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{

  
    public interface IFloatingGrid
    {
        public int Width { get; }
        public int Height { get; }
        public bool IsInBound(Vector2Int pos);
        public bool IsOutBound(Vector2Int pos);

        Vector2Int GetNeighbor(Vector2Int pos, int i);
    }
    
    
    public interface IFloatingGrid<T> : IFloatingGrid where T : IFloatingGridItem
    {
       
        public void Put(Vector2Int pos, T data);
        public void Put( T data);
        public T Get(Vector2Int pos);
        public T Get(int x, int y);

        public void Reset(Vector2Int pos);
        
        public IEnumerable<T> IterateAll { get; }
        
        public IObservable<T> ObservableOnChange { get; }
    }

    public interface IFloatingGridItem
    {
        public Vector2Int Position { get; set; }
        public int Id { get; set; }
    }
   
}