using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogues/Dialogue emotions", fileName = "DialogueEmotionsList")]
    public class Emotions : ScriptableObject
    {
        [Header("Quinn")]
        public string quinn_neutral_string = "QuinnNeutral";
        public string quinn_angry_string = "QuinnAngry";
        public string quinn_confused_string = "QuinnConfused";
        [Space]
        public Sprite transparent;
        public Sprite quinn_stop;
        public Sprite[] quinn_neutral = new Sprite[2];
        public Sprite[] quinn_angry = new Sprite[2];
        public Sprite[] quinn_confused = new Sprite[2];
        public Sprite[] quinn_nervous = new Sprite[2];
        [Space]
        [Header("Carolyn")]
        public string carolyn_neutral_string = "CarolynNeutral";
        [Space]
        public Sprite carolyn_stop;
        public Sprite[] carolyn_neutral = new Sprite[2];
    }
}
