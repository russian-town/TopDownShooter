using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class GameLoopService
    {
        private readonly Gameboard _gameboard;
        private readonly BulletViewFactory _bulletViewFactory;

        public GameLoopService(Gameboard gameboard, BulletViewFactory bulletViewFactory)
        {
            _gameboard = gameboard;
            _bulletViewFactory = bulletViewFactory;
        }

        public bool CanChangePosition(Vector3 worldPosition)
        {
            Vector2Int boardPosition =
                _gameboard.GetBoardFromWorldPosition(worldPosition);

            if (_gameboard.IsCellExist(boardPosition))
            {
                if (_gameboard.GetCellTypeFromBoardPosition(boardPosition) == CellType.Wall)
                    return false;

                return true;
            }

            return false;
        }

        public void Shoot()
        {
            Bullet bullet = new();
            _bulletViewFactory.Create(bullet);
        }
    }
}
