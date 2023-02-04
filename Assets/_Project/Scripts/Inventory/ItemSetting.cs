using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Inventory/Item Setting", fileName = "Setting")]
    public class ItemSetting : ScriptableObject
    {
        public string itemName, solution;
        public float pickupDistance;
        public Sprite inGame, inInventory;
        public bool collected;
    }
}