using Source.Codebase.Domain.Models;
using Source.Codebase.Services;
using UnityEngine;

public class GameboardService
{
    private readonly Gameboard _gameboard;
    private readonly WallViewFactory _wallViewFactory;

    public GameboardService(Gameboard gameboard, WallViewFactory wallViewFactory)
    {
        _gameboard = gameboard;
        _wallViewFactory = wallViewFactory;
    }

    public void GenerateWalls()
    {
        for (int x = 0; x < _gameboard.Width; x++)
        {
            for (int y = 0; y < _gameboard.Height; y++)
            {
                Vector2Int worldPosition = new(x, y);

                if (_gameboard.CanCreateWall(worldPosition))
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        Wall wall = new();
                        _wallViewFactory.Create(wall, worldPosition);
                    }
                }
            }
        }
    }
}
