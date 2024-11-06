using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    private void Awake()
    {
        StackController.onStackPlaced += StackPlacedCallBack;
    }
    private void OnDestroy()
    {
        StackController.onStackPlaced -= StackPlacedCallBack;
    }
    private void StackPlacedCallBack(GridCell gridCell)
    {
        StartCoroutine(StackPlacedCoroutine(gridCell));
    }
    IEnumerator StackPlacedCoroutine(GridCell gridCell)
    {
        yield return CheckForMerge(gridCell);
    }
    IEnumerator CheckForMerge(GridCell gridCell)
    {
        // Does this cell has neighbors ?
        List<GridCell> neighborGridCells = GetNeighborGridCells(gridCell);

        if (neighborGridCells.Count <= 0) yield break;

        Color gridCellTopHexagonColor = gridCell.Stack.GetTopHexagonColor();

        List<GridCell> similarNeighborGridCells = GetSimilarNeighborGridCells(gridCellTopHexagonColor, neighborGridCells.ToArray());

        if (similarNeighborGridCells.Count <= 0) yield break;

        // at the point, have a list of similar neighbors
        List<Hexagon> hexagonsToAdd = GetHexagonsToAdd(gridCellTopHexagonColor, similarNeighborGridCells.ToArray());


        //Remove the hexagons from their stacks
        RemoveHexagonsFromStacks(hexagonsToAdd, similarNeighborGridCells.ToArray());

        // at this point, have removed the stacks we dont need anymore
        // we have some free grid cells

        MoveedHexagons(gridCell, hexagonsToAdd);

        yield return new WaitForSeconds(.2f + (hexagonsToAdd.Count + 1) * .01f);

        

        yield return CheckForCompleteStack(gridCell, gridCellTopHexagonColor);
    }

    private List<GridCell> GetNeighborGridCells(GridCell gridCell)
    {
        LayerMask gridCellMask = 1 << gridCell.gameObject.layer;

        List<GridCell> neighborGridCells = new List<GridCell>();

        Collider[] neighborGridCellColliders = Physics.OverlapSphere(gridCell.transform.position, 2, gridCellMask);

        foreach (Collider col in neighborGridCellColliders)
        {
            GridCell neighborGridCell = col.GetComponent<GridCell>();

            if (!neighborGridCell.IsOccupied) continue;

            if (neighborGridCell == gridCell) continue;

            neighborGridCells.Add(neighborGridCell);
        }
        return neighborGridCells;
    }
    private List<GridCell> GetSimilarNeighborGridCells(Color gridCellTopHexagonColor, GridCell[] neighborGridCells)
    {
        List<GridCell> similarNeighborGridCells = new List<GridCell>();

        foreach (GridCell neighborGridCell in neighborGridCells)
        {
            Color neighborGridCellTopHexagonColor = neighborGridCell.Stack.GetTopHexagonColor();

            if (gridCellTopHexagonColor == neighborGridCellTopHexagonColor)
                similarNeighborGridCells.Add(neighborGridCell);
        }

        return similarNeighborGridCells;
    }
    private List<Hexagon> GetHexagonsToAdd(Color gridCellTopHexagonColor, GridCell[] similarNeighborGridCells)
    {
        List<Hexagon> hexagonsToAdd = new List<Hexagon>();

        foreach (var neighborCell in similarNeighborGridCells)
        {
            HexgonStack neighborCellHexStack = neighborCell.Stack;

            for (int i = neighborCellHexStack.hexagons.Count - 1; i >= 0; i--)
            {
                Hexagon hexagon = neighborCellHexStack.hexagons[i];

                if (hexagon.Color != gridCellTopHexagonColor) break;

                hexagonsToAdd.Add(hexagon);
                hexagon.SetParent(null);
            }
        }
        return hexagonsToAdd;
    }
    private void RemoveHexagonsFromStacks(List<Hexagon> hexagonsToAdd, GridCell[] similarNeighborGridCells)
    {
        foreach (var neighborCell in similarNeighborGridCells)
        {
            HexgonStack stack = neighborCell.Stack;

            foreach (var hexagon in hexagonsToAdd)
            {
                if (stack.Contains(hexagon)) stack.Remove(hexagon);
            }
        }
    }
    private void MoveedHexagons(GridCell gridCell, List<Hexagon> hexagonsToAdd)
    {
        float initialY = gridCell.Stack.hexagons.Count * .2f;

        for (int i = 0; i < hexagonsToAdd.Count; i++)
        {
            Hexagon hexagon = hexagonsToAdd[i];

            float targetY = initialY + i * .2f;
            Vector3 targetLocalPos = Vector3.up * targetY;

            gridCell.Stack.AddHexa(hexagon);
            hexagon.MoveToLocal(targetLocalPos);

        }
    }
    private IEnumerator CheckForCompleteStack(GridCell gridCell, Color topColor) 
    {
        if (gridCell.Stack.hexagons.Count < 10) yield break;

        List<Hexagon> similarHexagons = new List<Hexagon>();

        for(int i = gridCell.Stack.hexagons.Count - 1; i >=0; i--)
        {
            Hexagon hexagon = gridCell.Stack.hexagons[i];

            if (hexagon.Color != topColor) break;

            similarHexagons.Add(hexagon);
        }

        // at this point, have a list of similar hexagon
        // how many

        int similarHexagonCount = similarHexagons.Count;

        if (similarHexagons.Count < 10) yield break;

        float delay = 0;

        while(similarHexagons.Count > 0)
        {
            similarHexagons[0].SetParent(null);
            similarHexagons[0].Vanish(delay);
            //DestroyImmediate(similarHexagons[0].gameObject);

            delay += .01f; 

            gridCell.Stack.Remove(similarHexagons[0]);
            similarHexagons.RemoveAt(0);
        }

        yield return new WaitForSeconds(.2f + (similarHexagonCount + 1) * .01f);
    }

}
