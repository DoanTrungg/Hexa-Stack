using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Config/Level")]
public class RemoteLevel : Config<RemoteLevel>
{
    public int level;
    public int score;
    public List<Stack> listStack = new List<Stack>(); 
}

[System.Serializable] 
public class Stack
{
    public int colum;
    public int row;
    public bool hide;
    public bool block;
    public List<int> listColor = new List<int>();
}
