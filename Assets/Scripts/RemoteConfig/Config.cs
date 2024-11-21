using UnityEngine;

public class Config<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.FindObjectsOfTypeAll<T>()[0]; 
            }
            return _instance;
        }
    }
}
