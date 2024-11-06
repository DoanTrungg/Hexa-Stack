using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSpawner : MonoBehaviour
{
    [SerializeField] private Transform stackPosParent;
    [SerializeField] private GameObject hexagonPrefab;
    [SerializeField] private GameObject hexagonStackPrefab;

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
        GameObject hexStack = Instantiate(hexagonPrefab, parent.position, Quaternion.identity, parent);
        hexStack.name = $"Stack { parent.GetSiblingIndex() }";

        int amount = Random.Range(2, 7);
        for(int i = 0; i < amount; i++)
        {
            Vector3 hexagonLocal = Vector3.up * i * 0.2f;
            Vector3 spawnPos = hexStack.transform.TransformPoint(hexagonLocal);
            GameObject hexagonIns = Instantiate(hexagonPrefab, spawnPos, Quaternion.identity, hexStack.transform);
        }
    }
}
