using System;
using System.Collections.Generic;
using Alkacom.SDK;
using I2.Loc;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{

    public interface IGridWalkingData
    {
        public bool IsReachable(Vector2Int pos);
        public int Cost( Vector2Int neighbor);
    }
    public interface IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public bool IsInBound(Vector2Int pos);
        public bool IsOutBound(Vector2Int pos);

        Vector2Int GetNeighbor(Vector2Int pos, int i);
    }
    
    
    public interface IGrid<T> : IGrid
    {
       
        public void Put(Vector2Int pos, T data);
        public IGridData<T> Get(Vector2Int pos);
      
        public IEnumerable<IGridData<T>> IterateAll { get; }
        
        public IObservable<IGridData<T>> ObservableOnChange { get; }
    }

    public struct IGridData<T>
    {
        private readonly BehaviorSubject<T> _subject;
        public readonly Vector2Int Position;
        private T _data;
        public IGridData(T data, int x, int y)
        {
            Position = new Vector2Int(x, y);
            _data = data;
            _subject = new BehaviorSubject<T>(data);
            
        }

        public T Data => _subject.Value;
        public IObservable<T> Observable => _subject;

        public void Set(T data)
        {
            _data = data;
            _subject.OnNext(data);
        }

        public void SetSilent(T data) => Set(data);
    }

   
}