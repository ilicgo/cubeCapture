using Alkacom.SDK.CircularPool;
using UnityEngine;
using Zenject;

namespace Alkacom.Scripts.Particles
{
    public class ParticleGroupPool
    {
        private readonly ICircularPool<ParticleController>[] _pools;

        public ParticleGroupPool(ParticleSettings particleSettings, IFactory<GameObject, ParticleController> factory)
        {
            _pools = CreateGroupPools(particleSettings, factory);
        }

        ICircularPool<ParticleController>[] CreateGroupPools(ParticleSettings particleSettings, IFactory<GameObject, ParticleController> factory)
        {
            var groups = particleSettings.groups;
            var groupSize = groups.Length;
            ICircularPool<ParticleController>[] pools = new ICircularPool<ParticleController>[groupSize];

            for (int i = 0, imax = groupSize; i < imax; i++)
            {
                var group = groups[i];
                var index = (int) group.particleId;
                var pool = new CircularPool<ParticleController>(group.size, factory);
                pool.Instantiate(group.prefab);
                pools[index] = pool;
            }

            return pools;
        }

        public ICircularPool<ParticleController> GetPool(ParticleId particleId) => _pools[(int) particleId];
    }
}