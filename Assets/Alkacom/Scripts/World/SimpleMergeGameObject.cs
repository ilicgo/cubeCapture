using System;
using System.Collections.Generic;
using MTAssets.EasyMeshCombiner;
using UnityEngine;

namespace Alkacom.Scripts
{
    public sealed class SimpleMergeGameObject
    {
        private GameObject _root;
        private RuntimeMeshCombiner _rmc;
        private List<GameObject> _list;
        private readonly string _name;
        private readonly Transform _parent;

        public SimpleMergeGameObject(string name = "SimpleMergeGameObject", Transform parent = null)
        {
            _list = new List<GameObject>();
            _name = name;
            _parent = parent;
            


        }

        public void Add(GameObject go)
        {
            if (_rmc == null)
            {
                _root = new GameObject();
                _root.transform.SetParent(_parent);
                _root.name = _name;
                _rmc = _root.AddComponent<RuntimeMeshCombiner>();
                _rmc.afterMerge = RuntimeMeshCombiner.AfterMerge.DeactiveOriginalGameObjects;
                _rmc.addMeshColliderAfter = false;
            }
            _rmc.targetMeshes.Add(go);
            _list.Add(go);
            
        }

        public void Merge() => _rmc.CombineMeshes();

        public void Destroy()
        {
            GameObject.Destroy(_root);
            foreach (var go in _list)
                GameObject.Destroy(go);

            _rmc = null;
        }

        public GameObject GetRoot() => _root;
    }
}