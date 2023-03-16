using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [HideInInspector] public static InventoryUI instance;
        
        [Header("Inventory based variables")]
        [SerializeField] InventoryManager inventoryManager;
        public GameObject[] slots;
        [SerializeField] GameObject slotPrefab;
        
        int collectedItems;
        int itemCounter = 0;
        
        bool puzzleOpen;

        Transform parent;
        Transform gridSystem;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);

            parent = transform.GetChild(0);
            gridSystem = parent.transform.GetChild(0);
            collectedItems = inventoryManager.GetItems().Count();

            puzzleOpen = false;
        }

        public void Update()
        {
            collectedItems = inventoryManager.GetItems().Count();
            if (itemCounter != collectedItems)
            {
                itemCounter++;
                Array.Resize(ref slots, collectedItems);
                GameObject newSlotObject = Instantiate(slotPrefab, gridSystem);
                slots[collectedItems - 1] = newSlotObject;
                _SlotAndItemImage();
            }

            if (!puzzleOpen) _DeactivateInventory();
        }

        void _SlotAndItemImage()
        {
            for (int i = 0; i < itemCounter; i++)
            {
                var itemSetting = inventoryManager.items[i];

                GameObject slot = gridSystem.GetChild(i).gameObject;

                slots[i] = slot;

                if (slot.transform.childCount == 0) 
                {
                    continue;
                }

                var image = slot.transform.GetChild(0).GetComponent<Image>();
                image.sprite = itemSetting.inInventory;

                slot.layer = itemSetting.layerValue;

                var dragableItem = slot.transform.GetChild(0).GetComponent<DragableItem>();
                dragableItem.solution = itemSetting.solution;
            }
        }

        public void _DeactivateInventory()
        {
            puzzleOpen = false;
            for (int i = 0; i < itemCounter; i++)
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

        public void _ActivateInventory(int layerNumber)
        {
            puzzleOpen = true;
            for (int i = 0; i < itemCounter; i++)
            {
                if (slots[i].transform.childCount > 0 && layerNumber == slots[i].layer)
                {
                    var image = slots[i].transform.GetChild(0)?.GetComponent<Image>();
                    var parentImage = slots[i].transform.GetComponent<Image>();

                    if (image != null)
                    {
                        image.raycastTarget = true;
                        image.enabled = true;
                        image.gameObject.SetActive(true);
                    }

                    if (parentImage != null)
                    {
                        parentImage.raycastTarget = true;
                        parentImage.enabled = true;
                        parentImage.gameObject.SetActive(true);
                    }
                }
                else
                {
                    var parentImage = slots[i].transform.GetComponent<Image>();

                    if (parentImage != null)
                    {
                        parentImage.raycastTarget = false;
                        parentImage.enabled = false;
                        parentImage.gameObject.SetActive(false);
                    }
                }
            }
        }

        //public void _PuzzleOpen(bool config)
        //{
        //    if (config) puzzleOpen = true;
        //    else puzzleOpen = false;
        //}
    }
}