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
                if (slots[i].transform.childCount > 0)
                {
                    Image slotImage = slots[i].transform.GetComponent<Image>();
                    Image childImage = slots[i].transform.GetChild(0).GetComponent<Image>();

                    slotImage.raycastTarget = false;
                    slotImage.enabled = false;
                    childImage.raycastTarget = false;
                    childImage.enabled = false;
                }
                else
                {
                    Image slotImage = slots[i].transform.GetComponent<Image>();

                    slotImage.raycastTarget = false;
                    slotImage.enabled = false;
                }
            }
        }



        public void ActivateInventory()
        {
            for (int i = 0; i < amountOfItems; i++)
            {
                if (slots[i].transform.childCount > 0)
                {
                    var image = slots[i].transform.GetChild(0)?.GetComponent<Image>();
                    var parentImage = slots[i].transform.GetComponent<Image>();

                    if (image != null)
                    {
                        image.raycastTarget = true;
                        image.enabled = true;
                    }

                    if (parentImage != null)
                    {
                        parentImage.raycastTarget = true;
                        parentImage.enabled = true;
                    }
                }
                else
                {
                    var parentImage = slots[i].transform.GetComponent<Image>();

                    if (parentImage != null)
                    {
                        parentImage.raycastTarget = true;
                        parentImage.enabled = true;
                    }
                }
            }
        }


        public void IsPuzzleOpen(bool config)
        {
            if (config) puzzleOpen = true;
            else puzzleOpen = false;
        }
    }
}