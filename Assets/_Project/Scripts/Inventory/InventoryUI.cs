using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Inventory based variables")]
        [SerializeField] InventoryManager inventoryManager;
        [SerializeField] DragableItem[] dragableItem;
        [SerializeField] Image[] images;
        [SerializeField] GameObject[] slots;
        int amountOfChildren;
        //public string[] solutions;

        [Header("Activate solution puzzle")]
        public bool solutionActive = false;

        private void Start()
        {
            //find images in Children
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    Transform child = transform.GetChild(i);
            //    amountOfChildren += child.childCount;
            //}
            //dragableItem = new DragableItem[amountOfChildren];

            //images = new Image[amountOfChildren];
            //images = GetComponentsInChildren<Image>();

            //for (int i = 0; i < images.Length; i++)
            //{
            //    images[i] =
            //}
        }

        public void Update()
        {


            //Refresh();
            //if (Input.GetKeyDown(openInventory))
            //{
            //    inventory.SetActive(!inventory.activeSelf);
            //}
        }

        //public void Refresh()
        //{
        //    var items = inventoryManager.GetItems();
        //    //item count should not be longer than images
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        images[i].sprite = items[i].inInventory;

        //        if (solutionActive) dragableItem[i].solution = items[i].solution;
        //    }
        //}
    }
}