using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexgonStack : MonoBehaviour
{
    public List<Hexagon> hexagons = new List<Hexagon>();

    public void Place()
    {
        foreach (var hexa in hexagons)
        {
            hexa.DisableCollider(false);
        }
    }
    
}
