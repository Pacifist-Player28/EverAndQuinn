using UnityEngine;

namespace DialogueSystem {
    [System.Serializable]
    public class Dialogue
    {
        [TextArea]
        public string[] sentences;
    }
}