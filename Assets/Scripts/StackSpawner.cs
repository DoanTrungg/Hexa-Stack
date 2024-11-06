using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class StackSpawner : MonoBehaviour
{
    [SerializeField] private Transform stackPosParent;
    [SerializeField] private Hexagon hexagonPrefab;
    [SerializeField] private HexgonStack hexagonStackPrefab;

    [Header("Settings")]
    [NaughtyAttributes.MinMaxSlider(2, 8)]
    [SerializeField] private Vector2Int minMaxHexCount;
    [SerializeField] private Color[] listColor;
    private int stackCounter;

    private Pooling _pooling;

    private void Awake()
    {
        _pooling = Pooling.Instance();
        Application.targetFrameRate = 60;
        StackController.onStackPlaced += StackPlacedCallPack;
    }
    private void OnDestroy()
    {
        StackController.onStackPlaced -= StackPlacedCallPack;
    }

    private void StackPlacedCallPack(GridCell gidCell)
    {
        stackCounter++;

        if (stackCounter >= 3)
        {
            stackCounter = 0;
            GenerateStack();
        }
    }

    private void Start()
    {
        GenerateStack();
    }

    private void GenerateStack()
    {
        for(int i = 0; i < stackPosParent.childCount; i++)
        {
            Instantiate(hexagonStackPrefab).GenerateStack(stackPosParent.GetChild(i));
        }
    }
}
