using Source.Codebase.Controllers.Presenters.Abstract;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using UnityEngine;

namespace Source.Codebase.Controllers.Presenters
{
    public class EnemyPresenter : IPresenter
    {
        private readonly Enemy _enemy;
        private readonly EnemyView _enemyView;

        public EnemyPresenter(Enemy enemy, EnemyView enemyView, Vector3 enemySpawnPosition)
        {
            _enemy = enemy;
            _enemyView = enemyView;
            _enemy.SetWorldPosition(enemySpawnPosition);
            _enemyView.SetWorldPosition(enemySpawnPosition);
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
