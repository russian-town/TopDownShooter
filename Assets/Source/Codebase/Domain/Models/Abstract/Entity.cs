using System;
using UnityEngine;

namespace Source.Codebase.Domain.Models.Abstract
{
    public abstract class Entity
    {
        private readonly float _speed = 3f;
        private readonly float _smoothDamp = .1f;

        private float _currentVelocity;
        private Transform _gunEnd;

        public Vector2 WorldPosition { get; private set; }
        public float Angle { get; private set; }

        public event Action<Vector2> PositionChanged;

        public void SetGunEnd(Transform gunEnd)
            => _gunEnd = gunEnd;

        public void SetWorldPosition(Vector3 worldPosition)
            => WorldPosition = worldPosition;

        public void SetStartAngle(float angle)
            => Angle = angle;

        public void Move(Vector2 newWorldPosition)
        {
            Vector2 direction = newWorldPosition - WorldPosition;
            WorldPosition = newWorldPosition;
            float radiansAngle =
                Mathf.Atan2(direction.y, direction.x);
            float angle = radiansAngle * Mathf.Rad2Deg;
            Angle =
                Mathf.SmoothDampAngle(Angle, angle, ref _currentVelocity, _smoothDamp);
            PositionChanged?.Invoke(WorldPosition);
        }

        public Vector2[] GetTrajectory()
        {
            Vector2 origin = WorldPosition;
            Vector2 direction = _gunEnd.right;
            Vector2[] directions = new Vector2[20];

            for (int i = 0; i < 20; i++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, Mathf.Infinity);

                if (hitInfo)
                {
                    direction = Vector3.Reflect(direction, hitInfo.normal);
                    origin = hitInfo.point;
                    Debug.DrawLine(origin, origin + direction * hitInfo.distance, Color.red, 0.2f);
                }
                else
                {
                    break;
                }
            }

            return directions;
        }

        public Vector3 CalculatePosition(Vector2 direction)
        {
            return WorldPosition + _speed * Time.deltaTime * direction;
        }
    }
}
