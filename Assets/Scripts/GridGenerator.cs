using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject hexagon;

    [Header("Setting")]
    [OnValueChanged("GenerateGrid")]
    [SerializeField] private int gridSize;

    [Header("Setting Stack")]
    [SerializeField] private HexgonStack stackSpawn;
    public static Action<GridCell> onStackGenerate;


    private void Start()
    {
        GenerateStack();
    }
    private void GenerateGrid()
    {
        transform.Clear();

        for(int x = -gridSize; x < gridSize; x++)
        {
            for(int y = -gridSize; y < gridSize; y++)
            {
                Vector3 spawnPos = grid.CellToWorld(new Vector3Int(x, y, 0));
                if (spawnPos.magnitude > grid.CellToWorld(new Vector3Int(1, 0, 0)).magnitude * gridSize) continue;
                var hexa = Instantiate(hexagon, spawnPos, Quaternion.identity, transform);
                hexa.transform.rotation = Quaternion.Euler(x, 30, y);
            }
        }
        

    }
    private void GenerateStack()
    {
        if (gameObject.transform.childCount < 5) return;
        int amountStack = Random.Range(0, gameObject.transform.childCount-2);
        HashSet<int> listRandom = new HashSet<int>();
        for (int i = 0; i < amountStack; i++)
        {
            int random = Random.Range(0, gameObject.transform.childCount);
            if (listRandom.Contains(random)) return;
            listRandom.Add(random);
            var hexGridRandom = gameObject.transform.GetChild(random);
            

            var stack = Instantiate(stackSpawn);
            stack.GenerateStack(hexGridRandom);
            stack.Place();
            var gridCell = hexGridRandom.GetComponent<GridCell>();
            gridCell.AssignStack(stack);

            onStackGenerate?.Invoke(gridCell);

            stack = null;
            hexGridRandom = null;
        }
        listRandom.Clear();
    }
}
