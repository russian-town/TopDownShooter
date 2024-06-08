using Source.Codebase.Domain.Models;
using Source.Codebase.Services;
using UnityEngine;

public class WallRandomService
{
    private readonly Gameboard _gameboard;
    private readonly WallViewFactory _wallViewFactory;

    public WallRandomService(Gameboard gameboard, WallViewFactory wallViewFactory)
    {
        _gameboard = gameboard;
        _wallViewFactory = wallViewFactory;
    }

    public void GenerateWalls()
    {
        for (int y = 1; y < _gameboard.Height - 1; y++)
        {
            for (int x = 1; x < _gameboard.Width - 1; x++)
            {
                Vector2Int boardPosition = new(x, y);
                Vector2 worldPosition =
                       _gameboard.GetWorldFromBoardPosition(boardPosition);

                if (_gameboard.CanCreateWall(boardPosition))
                {
                    if (Random.Range(0, 4) == 1)
                    {
                        Wall wall = new();
                        _wallViewFactory.Create(wall, worldPosition);
                        _gameboard.CellBecomeWall(boardPosition);
                    }
                }
            }
        }
    }
}
