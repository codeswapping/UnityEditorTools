using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAPUnityEditorTools.Attributes;

public class HorizontalLineDemo : MonoBehaviour
{
    public int Speed;
    [HorizontalLine]
    [CustomList]
    public SAPUnityEditorTools.Tools.SAPList<int> allIndexes;
    public int JumpSpeed;
}
