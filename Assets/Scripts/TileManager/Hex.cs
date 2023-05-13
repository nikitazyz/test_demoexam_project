using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public event Action<Hex> Clicked;
    public event Action<Hex> MouseEnter;
    
    [SerializeField] private bool _walkable;
    private Vector2Int _pos;

    public bool Walkable => _walkable;
    public Vector2Int Pos => _pos;

    public void SetPos(Vector2Int pos)
    {
        _pos = pos;
    }

    private void OnMouseDown()
    {
        Debug.Log(_pos);
        Clicked?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        MouseEnter?.Invoke(this);
    }
}
