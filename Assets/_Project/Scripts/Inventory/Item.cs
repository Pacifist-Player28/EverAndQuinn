using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        public ItemSetting setting;
        public UnityEvent collect;

        private void Awake() => GetComponent<SpriteRenderer>().sprite = setting.game;

        private void OnMouseOver()
        {
            var player = FindObjectOfType<PlayerMovementKeyboard>().transform;
            if (Vector3.Distance(player.position, transform.position) > setting.pickupDistance) return;

            if (Input.GetMouseButtonDown(0))
            {
                var im = GetComponentInParent<InventoryManager>();
                im.AddItem(this);
                collect.Invoke();
            }
        }
    }
}