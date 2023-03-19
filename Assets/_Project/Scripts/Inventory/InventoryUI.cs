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
                if (slots[i].transform.childCount == 0 && slots[i].layer == layerNumber) // skip slots without child objects
                {
                    slots[i].GetComponent<Image>().enabled = true;
                    slots[i].GetComponent<Image>().raycastTarget = true;
                    slots[i].SetActive(true);
                    continue;
                }
                else if (slots[i].transform.childCount == 0 && slots[i].layer != layerNumber) slots[i].gameObject.SetActive(false);
                else
                {
                    Debug.Log("Slot: " + slots[i].name + i + " layer: " + slots[i].layer.ToString());
                    //Debug.Log("Itemcounter: " + itemCounter);
                    var slotImage = slots[i].transform.GetComponent<Image>();
                    var itemImage = slots[i].transform.GetChild(0)?.GetComponent<Image>();

                    if (layerNumber == slots[i].layer)
                    {
                        slotImage.raycastTarget = true;
                        slotImage.enabled = true;
                        slotImage.gameObject.SetActive(true);

                        itemImage.raycastTarget = true;
                        itemImage.enabled = true;
                        itemImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        itemImage.raycastTarget = false;
                        itemImage.enabled = false;
                        itemImage.gameObject.SetActive(false);

                        slotImage.raycastTarget = false;
                        slotImage.enabled = false;
                        slotImage.gameObject.SetActive(false);
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