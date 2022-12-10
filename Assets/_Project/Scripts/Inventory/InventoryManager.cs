using UnityEngine;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public ItemSetting[] items = new ItemSetting[7];

        public void AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item.setting;
                    item.setting.collected = true;
                    item.gameObject.SetActive(false);
                    return;
                }
            }
        }

        public ItemSetting[] GetItems()
        {        
            var allSettings = Resources.FindObjectsOfTypeAll<ItemSetting>();

            List<ItemSetting> collected = new();

            foreach (ItemSetting setting in allSettings)
            {
                if (setting.collected) collected.Add(setting);
            }

            return collected.ToArray();
        }
    }
}