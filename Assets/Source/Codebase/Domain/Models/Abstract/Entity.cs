using System;
using Source.Codebase.Domain.Configs;
using UnityEngine;

namespace Source.Codebase.Domain.Models.Abstract
{
    public abstract class Entity
    {
        private readonly float _speed = 3f;
        private readonly float _smoothDamp = .1f;

        private Vector3 _worldPosition;
        private float _currentVelocity;
        private EntityConfig _config;

        private Vector2 Forward =>
            new Vector2(_worldPosition.x, 0f).normalized;
        public float Angle { get; private set; }

        public event Action<Vector3> PositionChanged;

        public void SetWorldPosition(Vector3 worldPosition)
            => _worldPosition = worldPosition;

        public void SetStartAngle(float angle)
            => Angle = angle;

        public void SetConfig(EntityConfig entityConfig)
            => _config = entityConfig;

        public void Move(Vector3 newWorldPosition)
        {
            Vector3 direction = newWorldPosition - _worldPosition;
            _worldPosition = newWorldPosition;
            float targetAngle =
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Angle =
                Mathf.SmoothDampAngle(Angle, targetAngle, ref _currentVelocity, _smoothDamp);
            PositionChanged?.Invoke(_worldPosition);
        }

        public RaycastHit2D[] Shoot()
        {
            RaycastHit2D[] results = new RaycastHit2D[10];
            Debug.Log("Shoot");

            if (Physics2D.Raycast(
                _worldPosition,
                Vector2.right,
                _config.ContactFilter2D,
                results,
                Mathf.Infinity) > 0)
            {
                Debug.Log(results[0].transform.name);

                if (results[0].transform.TryGetComponent(out IDamageble damageble))
                {
                    damageble.TakeDamage(10f);
                    return results;
                }

                Vector3 cross = Vector3.Cross(results[0].point, results[0].normal);

                while (Physics2D.Raycast(
                    results[0].point,
                    cross,
                    _config.ContactFilter2D,
                    results,
                    Mathf.Infinity) > 0)
                {
                    Debug.Log(results[0].transform.name);
                }
            }

            return results;
        }

        public Vector3 CalculatePosition(Vector3 direction)
        {
           return _worldPosition + _speed * Time.deltaTime * direction;
        }
    }
}
