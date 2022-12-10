using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public InventoryManager im;
        public Image[] images;
        public KeyCode openInventory;
        public GameObject inventory;

        public void Update()
        {
            if (Input.GetKeyDown(openInventory))
            {
                Refresh();
                inventory.SetActive(!inventory.activeSelf);
            }
        }

        public void Refresh()
        {
            var items = im.GetItems();

            //item count should not be longer than images
            for (int i = 0; i < items.Length; i++)
            {
                images[i].sprite = items[i].inventory;
            }

        }
    }
}