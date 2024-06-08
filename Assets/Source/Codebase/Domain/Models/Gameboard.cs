using System;
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
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool CanCreateWall(Vector2Int boardPosition)
        {
            Cell cell = GetCell(boardPosition);

            if (cell.Type != CellType.Empty)
                return false;

            cell.BecomeWall();
            bool tryFindPath = FindPath();
            cell.BecomeEmpty();

            if (tryFindPath == false)
            {
                FindPath();
                return false;
            }

            FindPath();
            return true;
        }

        public void CellBecomeWall(Vector2Int boardPosition)
        {
            Cell cell = GetCell(boardPosition);
            cell.BecomeWall();
        }

        public Vector3 GetWorldFromBoardPosition(Vector2Int boardPosition)
        {
            float xOffset = (float)-Width / 2 + 0.5f;
            float yOffset = (float)-Height / 2 + 0.5f;

            Vector2 positionOffset = new(xOffset, yOffset);

            return new Vector2(boardPosition.x, boardPosition.y) + positionOffset;
        }

        public Vector2Int GetBoardFromWorldPosition(Vector3 worldPosition)
        {
            float xOffset = (float)-Width / 2 + 0.5f;
            float yOffset = (float)-Height / 2 + 0.5f;

            Vector2 positionOffset = new(xOffset, yOffset);

            Vector2 boardPosition = (Vector2)worldPosition - positionOffset;

            return new Vector2Int(Mathf.RoundToInt(boardPosition.x), Mathf.RoundToInt(boardPosition.y));
        }

        public CellType GetCellTypeFromBoardPosition(Vector2Int boardPosition)
            => GetCell(boardPosition).Type;

        public bool IsCellExist(Vector2Int boardPosition) =>
            boardPosition.x >= 0 && boardPosition.x < Width &&
            boardPosition.y >= 0 && boardPosition.y < Height;

        private Cell GetCell(Vector2Int boardPosition)
        {
            if (IsCellExist(boardPosition) == false)
                throw new Exception($"Cell with coordinates {boardPosition} does not exist!");

            return _cells[boardPosition.x, boardPosition.y];
        }

        private void CreateCells()
        {
            _cells = new Cell[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Cell cell = _cells[x, y] = new();
                    cell.SetBoardPosition(new(x, y));

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

            Cell center =
               _cells[_cells.GetLength(0) / 2, _cells.GetLength(1) / 2];
            center.BecomeDestination();
            FindPath();
        }

        private bool FindPath()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_cells[x, y].Type == CellType.Destination)
                        _searchFrontier.Enqueue(_cells[x, y]);
                    else
                        _cells[x, y].ClearPath();
                }
            }

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
