using UnityEngine;

namespace Source.Codebase.Domain.Models
{
    public class Cell
    {
        private Cell _north;
        private Cell _south;
        private Cell _east;
        private Cell _west;
        private Cell _nextOnPath;
        private CellType _cellType;
        private int _distance;
        private Vector2 _worldPosition;

        public bool HasPath => _distance != int.MaxValue;
        public bool IsAlternative { get; set; }
        public CellType CellType => _cellType;

        public void SetWorldPosition(Vector2 worldPosition)
            => _worldPosition = worldPosition;

        public void BecomeEmpty()
            => _cellType = CellType.Empty;

        public void BecomeDestination()
        {
            _cellType = CellType.Destination;
            _distance = 0;
            _nextOnPath = null;
        }

        public void BecomeWall()
            => _cellType = CellType.Wall;

        public static void MakeEastWestNeighbors(Cell east, Cell west)
        {
            west._east = east;
            east._west = west;
        }

        public static void MakeNorthSouthNeighbors(Cell north, Cell south)
        {
            north._south = south;
            south._north = north;
        }

        public void ClearPath()
        {
            _distance = int.MaxValue;
            _nextOnPath = null;
        }

        public Cell GrowPathTo(Cell neighbor)
        {
            if (!HasPath || neighbor == null || neighbor.HasPath)
                return null;

            neighbor._distance = _distance + 1;
            neighbor._nextOnPath = this;
            return neighbor.CellType != CellType.Wall ? neighbor : null;
        }

        public Cell GrowPathNorth() => GrowPathTo(_north);

        public Cell GrowPathEast() => GrowPathTo(_east);

        public Cell GrowPahtSouth() => GrowPathTo(_south);

        public Cell GrowPathWest() => GrowPathTo(_west);
    }
}
