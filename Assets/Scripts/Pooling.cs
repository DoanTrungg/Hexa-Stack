using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Pooling : Singleton<Pooling>
{
    public Transform hexagonPool;

    private List<Hexagon> listHexagon = new List<Hexagon>();

    public List<Hexagon> ListHexagon { get => listHexagon; set => listHexagon = value; }

    public void DestroyHexagon(Hexagon hexagon)
    {
        ListHexagon.Add(hexagon);
        hexagon.SetParent(hexagonPool);
        hexagon.transform.localPosition = Vector3.zero;
        hexagon.gameObject.SetActive(false);
        hexagon.HexagonStack = null;
    }
    public Hexagon GetHexagon()
    {
        var newHexagon = listHexagon[0];
        newHexagon.gameObject.SetActive(true);
        newHexagon.DisableCollider(true);
        newHexagon.transform.localScale = Vector3.one;
        listHexagon.RemoveAt(0);

        return newHexagon;
    }
}
