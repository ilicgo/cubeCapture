using System.Collections.Generic;
using System.Linq;
using Alkacom.SDK;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Alkacom.Scripts
{
    public sealed class ShapeDB : IShapeDB
    {
        private readonly IFactory<GameObject, IShapeRenderer> _factory;
        private readonly List<IShapeRenderer> _list;
        private GameObject _cellPrefab;
        private Random _rand;

        public ShapeDB(GameObject cellPrefab, IFactory<GameObject, IShapeRenderer> factory)
        {
            _rand = new Random();
            _cellPrefab = cellPrefab;
            _factory = factory;
            _list = new List<IShapeRenderer>();
        }
        public void Reset()
        {
            foreach (var iRenderer in _list)
                iRenderer.DestroySelf();
            
        }

        public void Build(ShapeDBDefinition data)
        {
            Reset();

            InternalBuild(data);
            InternalBuild(data);

           
            
        }

        private void InternalBuild(ShapeDBDefinition data)
        {
            foreach (var def in data.Iterator)
            {
            
                var entity = _factory.Create(_cellPrefab);
                entity.Build(def);
                _list.Add(entity);
            }
        }

        public Option<IShapeRenderer> Get()
        {
            var results = _list.Where(_ => _.IsFree).OrderBy(_ => _rand.Next()).Take(1).ToArray();
            if(results.Length == 0) return Option<IShapeRenderer>.None();
           
            return Option<IShapeRenderer>.Some(results[0]);
        }
    }
}