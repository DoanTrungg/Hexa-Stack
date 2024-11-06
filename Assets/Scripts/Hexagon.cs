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

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
    public void Configure(HexgonStack hexStack)
    {
        HexagonStack = hexStack;
    }
    public void MoveToLocal(Vector3 targetLocalPos)
    {
        LeanTween.cancel(gameObject);

        float delay = transform.GetSiblingIndex() * .01f;

        LeanTween.moveLocal(gameObject, targetLocalPos, .2f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(delay);

        Vector3 dicrection = (targetLocalPos - transform.localPosition).With(y: 0).normalized;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, dicrection);

        LeanTween.rotateAround(gameObject, rotationAxis, 180, .2f)
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(delay);
    }
    public void Vanish(float delay)
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.zero, .2f)
            .setEase(LeanTweenType.easeInBack)
            .setDelay(delay)
            .setOnComplete(() => Destroy(gameObject));
    }
}
