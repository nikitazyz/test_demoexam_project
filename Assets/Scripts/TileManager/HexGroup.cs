using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TileManager
{
    public class HexGroup : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private List<Hex> _tiles = new List<Hex>();
        private Hex[] currentPath;

        public void AddTile(Hex tile)
        {
            tile.MouseEnter += TileOnMouseEnter;
            _tiles.Add(tile);
        }

        private void TileOnMouseEnter(Hex obj)
        {
            currentPath = CalculatePath(_player.Pos, obj.Pos);
        }

        public Hex[] CalculatePath(Vector2Int start, Vector2Int end)
        {
            int h = (int)(end - start).magnitude;
            Cell cell = new Cell()
            {
                Hex = _tiles.Find(hex => hex.Pos == start),
                H = h,
                G = 0,
                F = h
            };
            Cell[] path = CalculatePath(end, new[] { cell });
            if (path == null)
            {
                return null;
            }
            return path.Select(cell => cell.Hex).ToArray();
        }

        private Cell[] CalculatePath(Vector2Int end, Cell[] path)
        {
            if (!_tiles.Find(hex => hex.Pos == end).Walkable)
            {
                return null;
            }

            if (path[^1].Hex.Pos == end)
            {
                return path;
            }

            Vector2Int offsetRight = new Vector2Int(1, 0);
            Vector2Int offsetLeft = new Vector2Int(-1, 0);
            Vector2Int offsetTop = new Vector2Int(0, 1);
            Vector2Int offsetBottom = new Vector2Int(0, -1);

            Cell cur = path?[^1];

            List<Hex> tilesAround = _tiles.Where(tile => (tile.Pos == cur.Hex.Pos + offsetRight 
                                                      || tile.Pos == cur.Hex.Pos + offsetLeft 
                                                      || tile.Pos == cur.Hex.Pos + offsetTop
                                                      || tile.Pos == cur.Hex.Pos + offsetBottom)
                                                      && tile.Walkable).ToList();
            if (tilesAround.Count == 0)
            {
                return null;
            }

            List<Cell> cells = new List<Cell>();
            foreach (var hex in tilesAround.Where(tile => path.All(p => p.Hex != tile)))
            {
                int h = (int)(end - hex.Pos).magnitude;
                int g = cur.G + 1;
                int f = h + g;
                cells.Add(new Cell()
                {
                    Hex = hex,
                    H = h,
                    G = g,
                    F = f
                });
            }

            foreach (var cell in cells.OrderBy(cell => cell.F))
            {
                Cell[] returnPath = CalculatePath(end, path.Concat(new[] { cell }).ToArray());
                if (returnPath == null)
                {
                    continue;
                }

                return returnPath;
            }

            return null;
        }

        private void OnDrawGizmos()
        {
            if (currentPath == null)
            {
                return;
            }
            for (int i = 1; i < currentPath.Length; i++)
            {
                Gizmos.DrawLine((Vector3)(Vector2)currentPath[i-1].transform.position, (Vector3)(Vector2)currentPath[i].transform.position);
            }
        }

        private class Cell
        {
            public Hex Hex;
            public int H;
            public int G;
            public int F;
        }
    }
}