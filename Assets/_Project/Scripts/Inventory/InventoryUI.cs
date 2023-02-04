using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Inventory based variables")]
        public InventoryManager inventoryManager;
        public Image[] images;
        public DragableItem[] slots;
        //public string[] solutions;

        [Header("Keycodes and Gameobjects")]
        public KeyCode openInventory;
        public GameObject inventory;

        [Header("Activate solution puzzle")]
        public bool solutionActive = false;

        public void Update()
        {
            Refresh();
            if (Input.GetKeyDown(openInventory))
            {
                inventory.SetActive(!inventory.activeSelf);
            }
        }

        public void Refresh()
        {
            var items = inventoryManager.GetItems();

            //item count should not be longer than images
            for (int i = 0; i < items.Length; i++)
            {
                images[i].sprite = items[i].inInventory;
                
                if(solutionActive) slots[i].solution = items[i].solution;
            }
        }
    }
}