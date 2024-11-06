using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexgonStack : MonoBehaviour
{
    public List<Hexagon> hexagons = new List<Hexagon>();
    [SerializeField] private Hexagon hexagonPrefab;

    [NaughtyAttributes.MinMaxSlider(2, 8)]
    [SerializeField] private Vector2Int minMaxHexCount;
    [SerializeField] private Color[] listColor;

    private Pooling _pooling;

    private void Awake()
    {
        _pooling = Pooling.Instance();
    }
    public void GenerateStack(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0,.2f,0);
        this.name = $"Stack {parent.GetSiblingIndex()}";

        Color stackColor = listColor[Random.Range(0, listColor.Length)];

        int amount = Random.Range(minMaxHexCount.x, minMaxHexCount.y);

        int firstColorHexagonCount = Random.Range(0, amount);

        Color[] colorArray = GetRandomColor();

        for (int i = 0; i < amount; i++)
        {
            Vector3 hexagonLocal = Vector3.up * i * 0.2f;
            Vector3 spawnPos = transform.TransformPoint(hexagonLocal);

            Hexagon hexagonIns;
            if (_pooling.ListHexagon.Count > 0)
            {
                hexagonIns = _pooling.GetHexagon();
                hexagonIns.transform.position = spawnPos;
                hexagonIns.transform.SetParent(transform);
            }
            else
            {
                hexagonIns = Instantiate(hexagonPrefab, spawnPos, Quaternion.identity, transform);
            }
            
            hexagonIns.Color = i < firstColorHexagonCount ? colorArray[0] : colorArray[1];
            hexagonIns.transform.rotation = Quaternion.Euler(hexagonIns.transform.position.x, 30, hexagonIns.transform.position.y);

            hexagonIns.Configure(this);

            AddHexa(hexagonIns);
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
    public void AddHexa(Hexagon hexagon)
    {
        hexagons.Add(hexagon);
        hexagon.SetParent(transform);
    }
    public Color GetTopHexagonColor()
    {
        return hexagons[hexagons.Count - 1].Color;
    }
    public void Place()
    {
        foreach (var hexa in hexagons)
        {
            hexa.DisableCollider(false);
        }
    }

    public bool Contains(Hexagon hexagon)
    {
        return hexagons.Contains(hexagon);
    }
    public void Remove(Hexagon hexagon)
    {
        hexagons.Remove(hexagon);

        if (hexagons.Count <= 0) DestroyImmediate(gameObject);
    }
    
}
