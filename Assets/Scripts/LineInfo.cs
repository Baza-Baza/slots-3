using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LineInfo : ScriptableObject
{
    public int[] indices = new int[5];
    public Color color = Color.white;
}
