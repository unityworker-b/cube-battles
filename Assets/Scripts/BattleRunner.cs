using System;
using Assets.Scripts.Cubes;
using Assets.Scripts.Cubes.Searching;
using Assets.Scripts.Cubes.Spawning;
using Zenject;

namespace Assets.Scripts
{
    class BattleRunner : ITickable, IDisposable
    {
        private IEnemySearcher _enemySearcher;
        private TickableManager _tickableManager;
        private Cube[] _cubes;

        [Inject]
        public void Construct(ICubeSpawner cubeSpawner, IEnemySearcher enemySearcher, TickableManager tickableManager)
        {
            _cubes = cubeSpawner.SpawnAll();
            _enemySearcher = enemySearcher;
            _tickableManager = tickableManager;

            tickableManager.Add(this);

            _enemySearcher.Initialize(_cubes);
        }

        public void Tick()
        {
            for (int i = 0, c = _cubes.Length; i < c; i++)
            {
                Cube cube = _cubes[i];

                if (cube.Alive)
                    cube.UpdateExact();
            }
        }

        public void Dispose()
        {
            _tickableManager.Remove(this);
        }
    }
}
