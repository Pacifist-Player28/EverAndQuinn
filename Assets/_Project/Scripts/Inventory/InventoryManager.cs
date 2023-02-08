using UnityEngine;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public ItemSetting[] items = new ItemSetting[14];

        public void AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item.setting;
                    item.setting.collected = true;
                    item.DeactivateItem();
                    return;
                }
            }
        }

        public ItemSetting[] GetItems()
        {
            var allSettings = Resources.FindObjectsOfTypeAll<ItemSetting>();

            List<ItemSetting> collected = new List<ItemSetting>();

            foreach (ItemSetting setting in allSettings)
            {
                if (setting.collected)
                {
                    bool itemExists = false;
                    collected.Add(setting);
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] == setting)
                        {
                            itemExists = true;
                            break;
                        }
                        else if (items[i] != null && items[i].layerName == gameObject.layer.ToString())
                        {
                            items[i] = setting;
                            itemExists = true;
                            break;
                        }
                    }
                    if (!itemExists)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (items[i] == null)
                            {
                                items[i] = setting;
                                break;
                            }
                        }
                    }
                }
            }
            return collected.ToArray();
        }
    }
}