using UnityEngine;

namespace Assets.Scripts.Cubes
{
    [CreateAssetMenu(fileName = "CubeParameters", menuName = "ScriptableObjects/CubeParameters")]
    public class CubeParameters : ScriptableObject
    {
        [SerializeField]
        private float _speed = 1;
        [SerializeField]
        private int _health = 100;
        [SerializeField]
        private int _weaponDamage = 1;
        [SerializeField]
        private float _weaponCooldownInterval = 10;
        [SerializeField]
        private Gradient _bodyColorSpreading; 

        public float Speed => _speed;
        public int Health => _health;
        public int WeaponDamage => _weaponDamage;
        public float WeaponCooldownInterval => _weaponCooldownInterval;
        public Gradient BodyColorSpreading => _bodyColorSpreading;
    }
}
