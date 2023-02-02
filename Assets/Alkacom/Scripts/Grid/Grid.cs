using System;
using System.Collections.Generic;
using Alkacom.SDK;
using UniRx;
using UnityEngine;

namespace Alkacom.Scripts
{
    public class Grid<T> : IGrid<T>
    {
        
        protected readonly int _width;
        protected readonly int _height;
        protected readonly IGridData<T>[,] _grid;
        private readonly BehaviorSubject<IGridData<T>> _subjectOnChange;
        
        private static readonly Vector2Int[] AllowMovements = new[]
        {
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1)
        };

       


        public Grid(int x, int y, T defaultCell)
        {
            _width = x;
            _height = y;
            _grid = new IGridData<T>[x,y];
            _subjectOnChange = new BehaviorSubject<IGridData<T>>(new IGridData<T>(default,0,0));
            
            for(int ix = 0, ixMax = x; ix <ixMax; ix++)
            for(int iy = 0, iyMax = y; iy <iyMax; iy++)
                Init(ix, iy, defaultCell);
            
        }


        public void Init(int x, int y, T data) => _grid[x, y] = new IGridData<T>(data, x,y);

        public void Put(Vector2Int pos, T data)
        {
            _grid[pos.x,pos.y].Set(data);
            _subjectOnChange.OnNext(_grid[pos.x,pos.y]);
        }

       
        public IGridData<T> Get(Vector2Int pos) => _grid[pos.x, pos.y];
        



        public int Width => _width;
        public int Height => _height;


        public IEnumerable<IGridData<T>> IterateAll => InternalIterateAll();
        public IObservable<IGridData<T>> ObservableOnChange => _subjectOnChange;

        private IEnumerable<IGridData<T>> InternalIterateAll()
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