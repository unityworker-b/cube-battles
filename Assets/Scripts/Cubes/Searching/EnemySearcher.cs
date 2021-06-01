using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Cubes.Searching
{
    class EnemySearcher : IEnemySearcher
    {
        private Vector3[] _positions;
        private Cube[] _cubes;
        private Dictionary<object, int> _cubeToIndexMap;

        public void Initialize(Cube[] cubes)
        {
            _cubes = cubes;
            _positions = new Vector3[cubes.Length];
            _cubeToIndexMap = new Dictionary<object, int>(cubes.Length);

            for (int i = 0, c = cubes.Length; i < c; i++)
            {
                Cube cube = cubes[i];
                _cubeToIndexMap[cube] = i;

                cube.PositionChanged += CubeOnPositionChanged;
                cube.Died += CubeOnDied;

                _positions[i] = cube.Position;
            }
        }

        public ITarget FindNearest(ITarget origin)
        {
            Cube nearestCube = null;
            float bestSqrDistance = float.MaxValue;
            Vector3 originPosition = origin.Position;

            for (int i = 0, c = _positions.Length; i < c; i++)
            {
                float sqrDistance = (_positions[i] - originPosition).sqrMagnitude;

                if (sqrDistance < bestSqrDistance)
                {
                    Cube cube = _cubes[i];

                    if (cube.Alive && !ReferenceEquals(cube, origin))
                    {
                        nearestCube = cube;
                        bestSqrDistance = sqrDistance;
                    }
                }
            }

            return nearestCube;
        }

        private void CubeOnPositionChanged(object sender, Vector3 position)
        {
            int index = _cubeToIndexMap[sender];
            _positions[index] = position;
        }

        private void CubeOnDied(object sender, EventArgs e)
        {
            Cube cube = (Cube)sender;
            cube.PositionChanged -= CubeOnPositionChanged;
            cube.Died -= CubeOnDied;
        }
    }
}
