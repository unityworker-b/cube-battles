using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Cubes.Spawning
{
    class SphericalCubeSpawner : ICubeSpawner
    {
        private static readonly float Phi = (Mathf.Sqrt(5.0f) + 1.0f) / 2.0f;

        private readonly CubeSpawnerParameters _parameters;
        private readonly IFactory<Cube> _cubeFactory;

        public SphericalCubeSpawner(CubeSpawnerParameters parameters, IFactory<Cube> cubeFactory)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _cubeFactory = cubeFactory ?? throw new ArgumentNullException(nameof(cubeFactory));
        }

        public Cube[] SpawnAll()
        {
            var cubes = new Cube[_parameters.Count];

            float spawnRadius = _parameters.Radius;

            for (int i = 0, c = _parameters.Count; i < c; i++)
            {
                Vector3 position = GetSphericalFibonacciPoint(i, c);
                Cube cube = _cubeFactory.Create();
                cube.Position = position * spawnRadius;
                cubes[i] = cube;
            }

            return cubes;
        }

        private static Vector3 GetSphericalFibonacciPoint(int index, int count)
        {
            float div = index / Phi;
            float phi = 2 * Mathf.PI * (div - Mathf.Floor(div));
            float cosPhi = Mathf.Cos(phi);
            float sinPhi = Mathf.Sin(phi);

            float z = 1.0f - (2.0f * index + 1.0f) / count;
            float theta = Mathf.Acos(z);
            float sinTheta = Mathf.Sin(theta);

            return new Vector3(cosPhi * sinTheta, z, sinPhi * sinTheta);
        }
    }
}
