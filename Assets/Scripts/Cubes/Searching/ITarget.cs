using UnityEngine;

namespace Assets.Scripts.Cubes.Searching
{
    interface ITarget
    {
        bool Alive { get; }

        Vector3 Position { get; }

        float Radius { get; }

        void TakeDamage(int amount);
    }
}
