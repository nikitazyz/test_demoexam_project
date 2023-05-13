using System;
using System.Collections;
using System.Collections.Generic;
using TileManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int _fieldSize;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private float _rowOffset;
    [SerializeField] private Hex _groundCell;
    [SerializeField] private Hex _waterCell;
    [SerializeField] private float _waterGenerationWeight = 0.5f;
    [SerializeField] private HexGroup _hexGroup;

    private List<Hex> _hexes = new List<Hex>();

    private void Start()
    {
        for (int y = 0; y < _fieldSize.y; y++)
        {
            for (int x = 0; x < _fieldSize.x; x++)
            {
                Vector2 pos = new Vector2(x * _cellSize.x, y * _cellSize.y);
                pos = (Vector2)transform.position + pos;
                if (y % 2 == 1)
                {
                    pos += Vector2.right * _rowOffset;
                }

                float noise = Mathf.PerlinNoise((pos.x*2) + Random.Range(0, 10000), (pos.y*2) + Random.Range(0, 10000));
                Hex generationCell = noise > _waterGenerationWeight ? _groundCell : _waterCell;
                Hex tile = Instantiate(generationCell, pos, Quaternion.identity);
                tile.SetPos(new Vector2Int(x, y));
                _hexes.Add(tile);
                _hexGroup.AddTile(tile);
            }
        }
    }
}
