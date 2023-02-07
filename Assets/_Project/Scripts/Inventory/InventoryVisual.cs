using Inventory;
using UnityEngine;
using UnityEngine.UI;

public class InventoryVisual : MonoBehaviour
{
    Image[] slots;
    [SerializeField] InventoryManager inventoryManager1;
    [SerializeField] InventoryManager inventoryManager2;

    private void Start()
    {
        slots = transform.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        AddItemIllustration();
    }

    void AddItemIllustration()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //BRO WTF THIS SHOULD NOT HAPPEN RIGTH????ß
            if (inventoryManager1.items[i].inInventory == null) return;
            slots[i].sprite = inventoryManager1.items[i].inInventory;
            if (inventoryManager2.items[i].inInventory == null) return;
            slots[i + 9].sprite = inventoryManager2.items[i + 9].inInventory;
        }
    }
}
