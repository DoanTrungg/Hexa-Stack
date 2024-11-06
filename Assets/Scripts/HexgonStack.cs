using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexgonStack : MonoBehaviour
{
    public List<Hexagon> hexagons = new List<Hexagon>();

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
