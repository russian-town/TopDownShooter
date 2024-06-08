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
        private CellType _type;
        private int _distance;
        private Vector2Int _boardPosition;

        public bool HasPath => _distance != int.MaxValue;
        public bool IsAlternative { get; set; }
        public CellType Type => _type;

        public void SetBoardPosition(Vector2Int boardPosition)
            => _boardPosition = boardPosition;

        public void BecomeEmpty()
            => _type = CellType.Empty;

        public void BecomeDestination()
        {
            _type = CellType.Destination;
            _distance = 0;
            _nextOnPath = null;
        }

        public void BecomeWall()
            => _type = CellType.Wall;

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
            return neighbor.Type != CellType.Wall ? neighbor : null;
        }

        public Cell GrowPathNorth() => GrowPathTo(_north);

        public Cell GrowPathEast() => GrowPathTo(_east);

        public Cell GrowPahtSouth() => GrowPathTo(_south);

        public Cell GrowPathWest() => GrowPathTo(_west);
    }
}
