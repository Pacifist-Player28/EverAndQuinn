using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea]
    public string[] sentences;

    public Sprite[] sprites;
}
