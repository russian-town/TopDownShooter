using Source.Codebase.Controllers.Presenters.Abstract;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using UnityEngine;

namespace Source.Codebase.Controllers.Presenters
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;
        private readonly Vector3 _direction;

        public BulletPresenter(Bullet bullet, BulletView view, Vector3 direction)
        {
            _bullet = bullet;
            _view = view;
            _view.SetSpeed(_bullet.Speed);
            _direction = direction;
        }

        public void Enable()
        {
            _view.Move(_direction);
        }

        public void Disable()
        {
        }
    }
}
