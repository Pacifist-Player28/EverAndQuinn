using UnityEngine;
using System.Collections.Generic;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public ItemSetting[] items = new ItemSetting[14];

        private void Awake()
        {
            
        }

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

            List<ItemSetting> collected = new();

            foreach (ItemSetting setting in allSettings)
            {
                if (setting.collected)
                {
                    collected.Add(setting);
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i] != null) continue;
                        else items[i] = setting;
                        break;
                    }
                }
            }
            //return collected.ToArray();
        }
    }
}