using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private LayerMask hexagonLayer;
    [SerializeField] private LayerMask gridHexagonLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
    private HexgonStack currentHexStack;
    private Vector3 currentStackInitPos;

    [Header("Data")]
    private GridCell targetCell;

    public static Action<GridCell> onStackPlaced;


    private void Update()
    {
        ManageControl();
    }
    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0)) { ManageMouseDown(); }
        else if (Input.GetMouseButton(0) && currentHexStack != null) { ManaeMouseDrag(); }
        else if (Input.GetMouseButtonUp(0) && currentHexStack != null) { ManageMouseUp(); }
    }
    private void ManageMouseDown()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickRay(), out hit, 500, hexagonLayer);

        if (hit.collider == null) return;
        currentHexStack = hit.collider.GetComponent<Hexagon>().HexagonStack;
        currentStackInitPos = currentHexStack.transform.position;
    }
    
    private void ManaeMouseDrag()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickRay(), out hit, 500, gridHexagonLayerMask);

        if(hit.collider == null)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveGridCell(hit);
        }
    }
    private void DraggingAboveGround()
    {
        RaycastHit hit;
        Physics.Raycast(GetClickRay(), out hit, 500, groundLayerMask);

        if (hit.collider == null) return;

        Vector3 currentStackTarget = hit.point.With(y: 2);

        currentHexStack.transform.position = Vector3.MoveTowards(
            currentHexStack.transform.position,
            currentStackTarget,
            Time.deltaTime * 30);
        targetCell = null;
    }
    private void DraggingAboveGridCell(RaycastHit hit)
    {
        GridCell gridCell = hit.collider.GetComponent<GridCell>();

        if(gridCell.IsOccupied)
        {
            DraggingAboveGround();
        }
        else
        {
            DraggingAboveNonOccupiedGridCell(gridCell);
        }
    }
    private void DraggingAboveNonOccupiedGridCell(GridCell gridCell)
    {
        Vector3 currentStackTarget = gridCell.transform.position.With(y: 2);

        currentHexStack.transform.position = Vector3.MoveTowards(
            currentHexStack.transform.position,
            currentStackTarget,
            Time.deltaTime * 30);

        targetCell = gridCell;
    }

    private void ManageMouseUp()
    {
        if(targetCell == null)
        {
            currentHexStack.transform.position = currentStackInitPos;
            currentHexStack = null;
            return;
        }

        currentHexStack.transform.position = targetCell.transform.position.With(y: .2f);
        currentHexStack.transform.SetParent(targetCell.transform);
        currentHexStack.Place();

        targetCell.AssignStack(currentHexStack);

        onStackPlaced?.Invoke(targetCell);

        targetCell = null;
        currentHexStack = null;
    }
    private Ray GetClickRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
