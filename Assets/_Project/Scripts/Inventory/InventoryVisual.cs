using Inventory;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    [SerializeField] Image[] slots;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] string layerNameUp;
    [SerializeField] string layerNameDown;
    int itemNow;
    int itemBefore = 0;

    private void Start()
    {
        var parent = transform.GetChild(0);
        slots = parent.transform.GetComponentsInChildren<Image>();

        itemNow = inventoryManager.GetItems().Count();

        //Debug.Log("Slots: " + slots[itemNow].name);
    }

    private void Update()
    {
        //Debug.Log("Item now: " + itemNow);
        //itemNow = inventoryManager.GetItems().Count();
        AddItemIllustration();
        if (itemNow != itemBefore)
        { 
            AddItemIllustration();
            itemBefore++;
        }
    }

    void AddItemIllustration()
    {
        //slots should have the same lenght as the InventoryManager
        for (int i = 0; i < slots.Length; i++)
        {
            //Debug.Log("i: " + i);
            //why does it not just ignore the NullReferenceException?
            if (inventoryManager.items[i].inInventory != null)
                slots[i].sprite = inventoryManager.items[i].inInventory;
            else break;
        }
    }
}
