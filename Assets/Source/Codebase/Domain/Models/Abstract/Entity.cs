using System;
using UnityEngine;

namespace Source.Codebase.Domain.Models.Abstract
{
    public abstract class Entity
    {
        private readonly float _speed = 3f;
        private readonly float _smoothDamp = .1f;

        private Vector3 _worldPosition;
        private float _currentVelocity;

        public float Angle { get; private set; }

        public event Action<Vector3> PositionChanged;

        public void SetWorldPosition(Vector3 worldPosition)
            => _worldPosition = worldPosition;

        public void SetStartAngle(float angle)
            => Angle = angle;

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

        public Vector3 CalculatePosition(Vector3 direction)
        {
           return _worldPosition + direction * _speed * Time.deltaTime;
        }
    }
}
