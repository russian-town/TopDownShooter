using System.Collections.Generic;
using UnityEngine;

namespace Source.Codebase.Domain.Models
{
    public class Gameboard
    {
        private readonly Queue<Cell> _searchFrontier;

        private Cell[,] _cells;

        public Gameboard(int width, int height)
        {
            _searchFrontier = new();
            Width = width;
            Height = height;
            CreateCells();
            FindPath();
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool CanCreateWall(Vector2Int position)
        {
            Cell cell = GetCellFromPosition(position);

            if (cell.CellType != CellType.Empty)
                return false;

            cell.BecomeWall();

            if (FindPath() == false)
            {
                cell.BecomeEmpty();
                FindPath();
                return false;
            }

            return true;
        }

        private Cell GetCellFromPosition(Vector2Int position)
        {
            return _cells[position.x, position.y];
        }

        private void CreateCells()
        {
            _cells = new Cell[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cell cell = _cells[x, y] = new();
                    cell.SetWorldPosition(new Vector2(x, y));

                    if (x > 0)
                        Cell.MakeEastWestNeighbors(cell, _cells[x - 1, y]);

                    if (y > 0)
                        Cell.MakeNorthSouthNeighbors(cell, _cells[x, y - 1]);

                    cell.IsAlternative = (x & 1) == 0;

                    if ((y & 1) == 0)
                        cell.IsAlternative = !cell.IsAlternative;

                    cell.BecomeEmpty();
                }
            }
        }

        private bool FindPath()
        {
            foreach (Cell cell in _cells)
            {
                cell.ClearPath();
            }

            Cell playerStartPosition = _cells[_cells.GetLength(0) / 2, _cells.GetLength(1) / 2];
            playerStartPosition.BecomeDestination();
            _searchFrontier.Enqueue(playerStartPosition);
            Cell enemyStartPosition = _cells[0, _cells.GetLength(1) / 2];
            enemyStartPosition.BecomeDestination();
            _searchFrontier.Enqueue(enemyStartPosition);

            if (_searchFrontier.Count == 0)
                return false;

            while (_searchFrontier.Count > 0)
            {
                Cell cell = _searchFrontier.Dequeue();

                if (cell != null)
                {
                    if (cell.IsAlternative)
                    {
                        _searchFrontier.Enqueue(cell.GrowPathNorth());
                        _searchFrontier.Enqueue(cell.GrowPahtSouth());
                        _searchFrontier.Enqueue(cell.GrowPathEast());
                        _searchFrontier.Enqueue(cell.GrowPathWest());
                    }
                    else
                    {
                        _searchFrontier.Enqueue(cell.GrowPathWest());
                        _searchFrontier.Enqueue(cell.GrowPathEast());
                        _searchFrontier.Enqueue(cell.GrowPahtSouth());
                        _searchFrontier.Enqueue(cell.GrowPathNorth());
                    }
                }
            }

            foreach (var cell in _cells)
            {
                if(cell.HasPath == false)
                    return false;
            }

            return true;
        }
    }
}
