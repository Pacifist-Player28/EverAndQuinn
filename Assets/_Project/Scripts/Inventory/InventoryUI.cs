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
        [SerializeField] DragableItem[] dragableItem;
        [SerializeField] Image[] images;
        [SerializeField] GameObject[] slots;
        [SerializeField] GameObject slotGameobject;
        [SerializeField] int amountOfItems;
        int previousAmountOfItems = 0;

        Transform parent;

        private void Start()
        {
            parent = transform.GetChild(0);
        }

        public void Update()
        {
            Debug.Log("previousAmountOfItems: " + previousAmountOfItems + " amountOfItems: " + amountOfItems);
            amountOfItems = inventoryManager.GetItems().Count();

            AddSlotIntoHierachy();

            if (previousAmountOfItems != amountOfItems)
            {
                slots = new GameObject[amountOfItems];
                Instantiate(slotGameobject, parent);
                //slots[amountOfItems].GetComponentInChildren<Image>().sprite = inventoryManager.items[amountOfItems].inInventory;
                previousAmountOfItems++;
                return;
            }
        }

        void AddSlotIntoHierachy()
        {
            for (int i = 0; i < previousAmountOfItems; i++)
            {
                slots[i] = parent.GetChild(i).gameObject;
                var item = slots[i].transform.GetChild(0);
                item.GetComponent<Image>().sprite = inventoryManager.items[i].inInventory;
            }
        }
    }
}
