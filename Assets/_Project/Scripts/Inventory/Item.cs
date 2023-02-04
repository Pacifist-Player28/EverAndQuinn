using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        public ItemSetting setting;
        public UnityEvent collect;

        private void Awake() => GetComponent<SpriteRenderer>().sprite = setting.inGame;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, setting.pickupDistance);
        }

        private void OnMouseOver()
        {
            var player = PlayerMovementKeyboard.instance.transform;
            if (Vector3.Distance(player.position, transform.position) > setting.pickupDistance) return;

            if (Input.GetMouseButtonDown(0))
            {
                var inventoryManager = GetComponentInParent<InventoryManager>();
                inventoryManager.AddItem(this);
                collect.Invoke();
            }
        }

        public void DeactivateItem()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}