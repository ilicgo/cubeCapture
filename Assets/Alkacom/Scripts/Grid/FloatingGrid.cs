using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{

    public class FloatingGrid<T> : IFloatingGrid<T> where T : IFloatingGridItem
    {
        
        protected readonly int _width;
        protected readonly int _height;
        protected readonly T[,] _grid;
        private readonly BehaviorSubject<T> _subjectOnChange;
        
        private static readonly Vector2Int[] AllowMovements = new[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1)
        };

        private readonly T _defaultCell;


        public FloatingGrid(int x, int y, T defaultCell)
        {
            _defaultCell = defaultCell;
            _width = x;
            _height = y;
            _grid = new T[x,y];
            _subjectOnChange = new BehaviorSubject<T>(defaultCell);
            
            for(int ix = 0, ixMax = x; ix <ixMax; ix++)
            for(int iy = 0, iyMax = y; iy <iyMax; iy++)
                Init(ix, iy, defaultCell);
            
        }


        public void Init(int x, int y, T data)
        {
            data.Position = new Vector2Int(x, y);
            _grid[x, y] = data;
        }

        public void Put(Vector2Int pos, T data)
        {
            data.Position = pos;
            _grid[pos.x, pos.y] = data;
            _subjectOnChange.OnNext(data);
        }
        
        public void Put( T data)
        {
            
            _grid[ data.Position.x,  data.Position.y] = data;
            _subjectOnChange.OnNext(data);
        }

       
        public T Get(Vector2Int pos) => _grid[pos.x, pos.y];
        public T Get(int x, int y) => _grid[x,y];
        public void Reset(Vector2Int pos) => Put(pos, _defaultCell);

        public int Width => _width;
        public int Height => _height;


        public IEnumerable<T> IterateAll => InternalIterateAll();
        public IObservable<T> ObservableOnChange => _subjectOnChange;

        private IEnumerable<T> InternalIterateAll()
        {
            for(int ix = 0, ixMax = _width; ix <ixMax; ix++)
            for(int iy = 0, iyMax = _height; iy <iyMax; iy++)
                yield return _grid[ix,iy];
        }

        public bool IsOutBound(Vector2Int pos) => !IsInBound(pos);
        public Vector2Int GetNeighbor(Vector2Int pos, int index) => pos + AllowMovements[index];

        public bool IsInBound(Vector2Int p)
        {
            if (p.x < 0 || p.y < 0 || p.x > _width - 1 || p.y > _height - 1) return false;
            return true;
        }
    }
}