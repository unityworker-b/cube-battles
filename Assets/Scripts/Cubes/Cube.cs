using System;
using Assets.Scripts.Cubes.Searching;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Cubes
{
    class Cube : MonoBehaviour, ITarget
    {
        private CubeParameters _cubeParameters;
        private IEnemySearcher _enemySearcher;

        private int _remainingHealth;
        private GameObject _gameObject;
        private Transform _transform;
        private Material _material;
        private float _cubeRadius;

        private ITarget _target;
        private float _lastTimeWeaponFired = float.MinValue;

        public bool Alive => _remainingHealth != 0;
        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public float Radius => _cubeRadius;

        public event EventHandler<Vector3> PositionChanged;
        public event EventHandler Died;

        [Inject]
        public void Construct(CubeParameters cubeParameters, IEnemySearcher searcher)
        {
            _cubeParameters = cubeParameters;
            _enemySearcher = searcher;

            _remainingHealth = cubeParameters.Health;
            _gameObject = gameObject;
            _transform = gameObject.transform;

            var meshRenderer = GetComponentInChildren<MeshRenderer>();
            _material = meshRenderer.material;
            _cubeRadius = meshRenderer.bounds.extents.magnitude;

            UpdateBodyColor();
        }

        public void UpdateExact()
        {
            if (_target == null || !_target.Alive)
            {
                _target = _enemySearcher.FindNearest(this);

                if (_target == null)
                    return;
            }

            Vector3 myPosition = _transform.position;
            Vector3 targetPosition = _target.Position;

            Vector3 offset = targetPosition - myPosition;
            float offsetLength = offset.magnitude;
            float desiredDistance = offsetLength - _cubeRadius - _target.Radius;

            if (desiredDistance > 1E-1f)
            {
                float maxDistanceDelta = _cubeParameters.Speed;

                if (maxDistanceDelta > desiredDistance)
                    maxDistanceDelta = desiredDistance;

                Vector3 movePosition = myPosition + offset * (desiredDistance / offsetLength);
                Vector3 newMyPosition = Vector3.MoveTowards(myPosition, movePosition, maxDistanceDelta);

                _transform.position = newMyPosition;

                PositionChanged?.Invoke(this, newMyPosition);
            }
            else if (Time.time - _lastTimeWeaponFired > _cubeParameters.WeaponCooldownInterval)
            {
                _lastTimeWeaponFired = Time.time;
                _target.TakeDamage(_cubeParameters.WeaponDamage);
            }
        }

        public void TakeDamage(int amount)
        {
            if (_remainingHealth == 0)
                return;

            _remainingHealth -= amount;

            if (_remainingHealth <= 0)
            {
                _remainingHealth = 0;
                _gameObject.SetActive(false);
                return;
            }

            UpdateBodyColor();
        }

        private void UpdateBodyColor()
        {
            float relativeHealth = _remainingHealth / (float)_cubeParameters.Health;
            _material.color = _cubeParameters.BodyColorSpreading.Evaluate(relativeHealth);
        }

        public class Factory : PlaceholderFactory<Cube>
        {
        }
    }
}
