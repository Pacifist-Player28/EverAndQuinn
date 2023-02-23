using System;
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
        bool puzzleOpen;

        Transform parent;
        Transform childSlot;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);
            parent = transform.GetChild(0);
            childSlot = parent.transform.GetChild(0);
            amountOfItems = inventoryManager.GetItems().Count();

            puzzleOpen = false;
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
                Instantiate(slotGameobject, childSlot);
                SlotAndItemImage();
            }
            if (puzzleOpen) ActivateInventory();
            else DeactivateInventory();
        }

        void SlotAndItemImage()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                var im = inventoryManager.items[i];

                slots[i] = childSlot.GetChild(i).gameObject;

                if (slots[i].transform.GetChild(0) == null) return;
                else
                {
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = im.inInventory;
                    slots[i].layer = im.layerValue;
                    slots[i].transform.GetChild(0).GetComponent<DragableItem>().solution = im.solution;
                }
            }
        }

        public void DeactivateInventory()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                var image = slots[i].transform.GetChild(0).GetComponent<Image>();
                var parents = slots[i].transform.GetComponent<Image>();

                image.raycastTarget = false;
                image.enabled = false;
                //Debug.Log("Item");
                parents.raycastTarget = false;
                parents.enabled = false;
                //Debug.Log("Slot");
            }
        }

        public void ActivateInventory()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                var image = slots[i].transform.GetChild(0).GetComponent<Image>();
                var parents = slots[i].transform.GetComponent<Image>();

                image.raycastTarget = true;
                image.enabled = true;

                parents.raycastTarget = true;
                parents.enabled = true;
            }
        }

        public void IsPuzzleOpen(bool config)
        {
            if (config) puzzleOpen = true;
            else puzzleOpen = false;
        }
    }
}