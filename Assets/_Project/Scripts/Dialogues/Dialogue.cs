using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem {
    [System.Serializable]
    public class Dialogue
    {
        public string name;

        [TextArea]
        public string[] sentences;

        public Sprite[] spritesRight;
        public Sprite[] spritesLeft;
    }
}
