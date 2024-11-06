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

    private void Start()
    {
        GenerateStack();
    }

    private void GenerateStack()
    {
        for(int i = 0; i < stackPosParent.childCount; i++)
        {
            GenerateStack(stackPosParent.GetChild(i));
        }
    }

    private void GenerateStack(Transform parent)
    {
        HexgonStack hexStack = Instantiate(hexagonStackPrefab, parent.position, Quaternion.identity, parent);
        hexStack.name = $"Stack { parent.GetSiblingIndex() }";

        Color stackColor = listColor[Random.Range(0, listColor.Length)];

        int amount = Random.Range(minMaxHexCount.x, minMaxHexCount.y);

        int firstColorHexagonCount = Random.Range(0,amount);

        Color[] colorArray = GetRandomColor();

        for(int i = 0; i < amount; i++)
        {
            Vector3 hexagonLocal = Vector3.up * i * 0.2f;
            Vector3 spawnPos = hexStack.transform.TransformPoint(hexagonLocal);
            
            Hexagon hexagonIns = Instantiate(hexagonPrefab, spawnPos, Quaternion.identity, hexStack.transform);
            hexagonIns.Color = i < firstColorHexagonCount ? colorArray[0] : colorArray[1];
            hexagonIns.transform.rotation = Quaternion.Euler(hexagonIns.transform.position.x, 30, hexagonIns.transform.position.y);

            hexagonIns.Configure(hexStack);

            hexStack.hexagons.Add(hexagonIns);
        }
    }

    private Color[] GetRandomColor()
    {
        List<Color> colors = new List<Color>();
        colors.AddRange(listColor);

        if (colors.Count <= 0) return null;
        Color firstColor = colors.OrderBy(x => Random.value).First();
        colors.Remove(firstColor);

        if (colors.Count <= 0) return null;
        Color secondColor = colors.OrderBy(x => Random.value).First();

        return new Color[] { firstColor, secondColor };
    }
}
