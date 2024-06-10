using Source.Codebase.Presentation.Abstract;
using UnityEngine;

namespace Source.Codebase.Presentation
{
    public class BulletView : ViewBase
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _speed;
        private Vector3 _direction;

        public void SetSpeed(float speed)
            => _speed = speed;

        public void Move(Vector3 direction)
        {
            _direction = direction;
            _rigidbody.velocity = direction * _speed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 reflect = Vector2.Reflect(_direction.normalized, normal);
            Move(reflect);
        }
    }
}
