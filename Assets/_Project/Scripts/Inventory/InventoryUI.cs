using System.Linq;
using UnityEditor.Graphs;
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

        private void Start()
        {
            parent = transform.GetChild(0);
            SlotAndItemImage();
        }

        public void Update()
        {
            Debug.Log("previousAmountOfItems: " + previousAmountOfItems + " amountOfItems: " + amountOfItems);
            //only call this when picking up an item
            amountOfItems = inventoryManager.GetItems().Count();

            if (previousAmountOfItems != amountOfItems)
            {
                slots = new GameObject[amountOfItems];
                Instantiate(slotGameobject, parent);
                SlotAndItemImage();
                previousAmountOfItems++;
            }
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
