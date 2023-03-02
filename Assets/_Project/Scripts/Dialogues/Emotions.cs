using UnityEngine;
using System.Collections.Generic;

namespace DialogueSystem
{
    [CreateAssetMenu(menuName = "Dialogues/Dialogue emotions", fileName = "DialogueEmotionsList")]
    public class Emotions : ScriptableObject
    {
        [Header("Transparent")]
        public string string_transparent = "transparent";
        [Header("Quinn")]
        public string string_QuinnNeutral = "QuinnNeutral";
        public string string_QuinnAngry = "QuinnAngry";
        public string string_QuinnConfused = "QuinnConfused";
        public string string_QuinnNervous = "QuinnNervous";
        public string string_QuinnSurprised = "QuinnSurprised";
        [Space]
        public Sprite transparentSprite;
        [Space]
        public Sprite sprite_QuinnStop;
        public Sprite[] sprite_QuinnNeutral = new Sprite[2];
        public Sprite[] sprite_QuinnAngry = new Sprite[2];
        public Sprite[] sprite_QuinnConfused = new Sprite[2];
        public Sprite[] sprite_QuinnNervous = new Sprite[2];
        public Sprite[] sprite_Quinnsurprised = new Sprite[2];
        [Space]
        [Header("Carolyn")]
        public string carolyn_neutral_string = "CarolynNeutral";
        [Space]
        public Sprite carolyn_stop;
        public Sprite[] carolyn_neutral = new Sprite[2];

        public Dictionary<string, Sprite[]> spriteDictionary = new Dictionary<string, Sprite[]>();

        private void OnEnable()
        {
            spriteDictionary.Add(string_transparent, new Sprite[] { transparentSprite, transparentSprite });
            spriteDictionary.Add(string_QuinnNeutral, sprite_QuinnNeutral);
            spriteDictionary.Add(string_QuinnAngry, sprite_QuinnAngry);
            spriteDictionary.Add(string_QuinnConfused, sprite_QuinnConfused);
            spriteDictionary.Add(string_QuinnNervous, sprite_QuinnNervous);
            spriteDictionary.Add(string_QuinnSurprised, sprite_Quinnsurprised);
            spriteDictionary.Add(carolyn_neutral_string, carolyn_neutral);
        }
    }
}
