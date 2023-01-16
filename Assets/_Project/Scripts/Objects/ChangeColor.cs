using UnityEngine;

namespace ColorChange
{
    public class ChangeColor : MonoBehaviour
    {
        public float colorToAdd;

        private Color startColor;
        private SpriteRenderer thisImage;

        private void Start()
        {
            thisImage = GetComponent<SpriteRenderer>();
            startColor = thisImage.color;
        }

        private void OnMouseEnter()
        {
            thisImage.color = Color.Lerp(startColor, Color.white, colorToAdd);
        }

        private void OnMouseExit()
        {
            thisImage.color = startColor;
        }
    }
}
