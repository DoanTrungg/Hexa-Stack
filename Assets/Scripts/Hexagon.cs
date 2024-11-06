using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private Collider collider;
    private HexgonStack hexagonStack;


    public Color Color
    {
        get => renderer.material.color;
        set => renderer.material.color = value;
    }
    public void DisableCollider(bool able)
    {
        collider.enabled = able;
    }
    public HexgonStack HexagonStack { get => hexagonStack; set => hexagonStack = value; }

    public void Configure(HexgonStack hexStack)
    {
        HexagonStack = hexStack;
    }
}
