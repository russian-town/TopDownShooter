using Source.Codebase.Domain;
using Source.Codebase.Domain.Models;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class GameLoopService
    {
        private readonly Gameboard _gameboard;

        public GameLoopService(Gameboard gameboard)
        {
            _gameboard = gameboard;
        }

        public bool CanChangePosition(Vector3 worldPosition)
        {
            Vector2Int boardPosition =
                _gameboard.GetBoardFromWorldPosition(worldPosition);

            if (_gameboard.IsCellExist(boardPosition))
            {
                if (_gameboard.GetCell(boardPosition).Type == CellType.Wall)
                    return false;

                return true;
            }

            return false;
        }
    }
}
