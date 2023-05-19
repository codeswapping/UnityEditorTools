using UnityEngine;
using SAPUnityEditorTools.Attributes;

public class HorizontalLineDemo : SerializableMonoBehaviour
{
    [HorizontalLine]
    public int Speed;
    public int JumpSpeed;
    [HorizontalLine]
    public int score;

    [HorizontalLine]
    [SerializableDictionaryProperty("#732fff")]
    public SerializableDictionary<string, Player> myPlayerDictonary;

    public GameObject[,] gridObjects = new GameObject[10,10];
}

[System.Serializable]
public class Player
{
    public int playerId;
    public int playerHealth;
}
