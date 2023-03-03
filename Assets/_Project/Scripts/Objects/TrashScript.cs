//Use this script in order to change the sprite inside a sprite list at the START of the scene.
namespace trash 
{
    using UnityEngine;

    public class TrashScript : MonoBehaviour
    {
        [SerializeField] Sprite[] trashSprites;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = trashSprites[Random.Range(0, trashSprites.Length)];
        }
    }

}