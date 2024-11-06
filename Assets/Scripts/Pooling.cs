using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Pooling : Singleton<Pooling>
{
    public Transform hexagonPool;
    public Transform stackPool;

    private List<Hexagon> listHexagon = new List<Hexagon>();
    private List<HexgonStack> listStak = new List<HexgonStack>();

    public List<Hexagon> ListHexagon { get => listHexagon; set => listHexagon = value; }
    public List<HexgonStack> ListStak { get => listStak; set => listStak = value; }

    //public void DestroyStack(HexgonStack stack)
    //{
    //    ListStak.Add(stack);
    //    stack.transform.SetParent(stackPool);
    //    stack.transform.localPosition = Vector3.zero;
    //    stack.gameObject.SetActive(false);
    //}
    //public HexgonStack GetHexgonStack()
    //{
    //    var newStack = ListStak[0];
    //    newStack.gameObject.SetActive(true);
    //    ListStak.RemoveAt(0);
    //    return newStack;
    //}

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
