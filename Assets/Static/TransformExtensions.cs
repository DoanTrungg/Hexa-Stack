using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    // Destroy all of the transform's children
   public static void Clear(this Transform transform)
    {
        while(transform.childCount > 0)
        {
            Transform childTrans = transform.transform.GetChild(0);
            childTrans.SetParent(null);
            Object.DestroyImmediate(childTrans.gameObject);
        }
    }
}
