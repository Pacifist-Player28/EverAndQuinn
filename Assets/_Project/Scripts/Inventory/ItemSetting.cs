using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item Setting", fileName = "Setting")]
    public class ItemSetting : ScriptableObject
    {
        public string itemName, solution;
        public string layerName;
        public int layerValue;
        public float pickupDistance;
        public Sprite inGame, inInventory;
        public bool collected;
    }
}