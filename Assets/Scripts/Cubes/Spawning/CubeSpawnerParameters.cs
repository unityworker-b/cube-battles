using UnityEngine;

namespace Assets.Scripts.Cubes.Spawning
{
    [CreateAssetMenu(fileName = "SpawnParameters", menuName = "ScriptableObjects/SpawnParameters")]
    public class CubeSpawnerParameters : ScriptableObject
    {
        [SerializeField]
        private int _count;

        [SerializeField]
        private float _radius;

        public int Count => _count;
        public float Radius => _radius;
    }
}
