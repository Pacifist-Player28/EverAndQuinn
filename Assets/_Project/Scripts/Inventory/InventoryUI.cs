using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Inventory based variables")]
        [SerializeField] InventoryManager inventoryManager;
        [SerializeField] GameObject[] slots;
        [SerializeField] GameObject slotGameobject;
        [SerializeField] int amountOfItems;
        int previousAmountOfItems = 0;

        Transform parent;

        private void Awake()
        {
            parent = transform.GetChild(0);
        }

        public void LateUpdate()
        {
            Debug.Log("previousAmountOfItems: " + previousAmountOfItems + " amountOfItems: " + amountOfItems);

            if (previousAmountOfItems != amountOfItems)
            {
                slots = new GameObject[amountOfItems];
                Instantiate(slotGameobject, parent);
                Debug.Log("Instantiatet!");
                SlotAndItemImage();
                previousAmountOfItems++;
            }

            //only call this when picking up an item
            amountOfItems = inventoryManager.GetItems().Count();
        }

        void SlotAndItemImage()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                slots[i] = parent.GetChild(i).gameObject;
                var item = slots[i].transform.GetChild(0);
                item.GetComponent<Image>().sprite = inventoryManager.items[i].inInventory;
            }
        }
    }
}
