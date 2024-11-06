using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject hexagon;

    [Header("Setting")]
    [OnValueChanged("GenerateGrid")]
    [SerializeField] private int gridSize;

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
}