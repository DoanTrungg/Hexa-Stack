using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private HexgonStack stack;
    public bool IsOccupied
    {
        get => stack != null;
        private set { }
    }

    public void AssignStack(HexgonStack stack)
    {
        this.stack = stack;
    }
}
