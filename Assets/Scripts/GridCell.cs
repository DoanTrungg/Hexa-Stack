using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private HexgonStack stack;
    public bool IsOccupied
    {
        get => Stack != null;
        private set { }
    }

    public HexgonStack Stack { get => stack; set => stack = value; }

    public void AssignStack(HexgonStack stack)
    {
        this.Stack = stack;
    }
}
