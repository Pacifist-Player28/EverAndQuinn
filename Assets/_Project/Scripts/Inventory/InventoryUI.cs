using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI instance;
        [Header("Inventory based variables")]
        [SerializeField] InventoryManager inventoryManager;
        public GameObject[] slots;
        [SerializeField] GameObject slotGameobject;
        [SerializeField] int amountOfItems;
        int previousAmountOfItems = 0;

        Transform parent;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);
            parent = transform.GetChild(0);
            amountOfItems = inventoryManager.GetItems().Count();
        }

        public void LateUpdate()
        {
            //Debug.Log("previousAmountOfItems: " + previousAmountOfItems + " amountOfItems: " + amountOfItems);
            //only call this when picking up an item
            amountOfItems = inventoryManager.GetItems().Count();

            if (previousAmountOfItems != amountOfItems)
            {
                previousAmountOfItems++;
                slots = new GameObject[amountOfItems];
                Instantiate(slotGameobject, parent);
                SlotAndItemImage();
            }
        }

        void SlotAndItemImage()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                var im = inventoryManager.items[i];

                slots[i] = parent.GetChild(i).gameObject;

                var item = slots[i].transform.GetChild(0);

                item.GetComponent<Image>().sprite = im.inInventory;
                slots[i].layer = im.layerValue;
                slots[i].transform.GetChild(0).GetComponent<DragableItem>().solution = im.solution;
            }
        }
    }
}
