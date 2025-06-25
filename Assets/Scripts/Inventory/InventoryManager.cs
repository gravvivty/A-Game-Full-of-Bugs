using UnityEngine;
using System.Collections.Generic;

namespace Project.Inventory
{
    /// <summary>
    /// Singleton class that manages the player's inventory by storing and checking collected items.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private List<ItemData> items = new List<ItemData>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

        }

        /// <summary>
        /// Adds an item to the inventory and logs its details.
        /// </summary>
        /// <param name="itemData">The item to be added.</param>
        public void AddItem(ItemData itemData)
        {
            items.Add(itemData);
            Debug.Log($"Picked up item: {itemData.itemName} (ID: {itemData.itemID})");
                
            Object.FindFirstObjectByType<AudioManager>().Play("new_item");
        }

        /// <summary>
        /// Checks whether an item with a specific ID exists in the inventory.
        /// </summary>
        /// <param name="itemID">The ID of the item to check for.</param>
        /// <returns>True if the item is found, false otherwise.</returns>
        public bool HasItemWithID(int itemID)
        {
            foreach (ItemData item in items)
            {
                if (item.itemID == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether an item with a specific name exists in the inventory.
        /// </summary>
        /// <param name="itemName">The name of the item to check for.</param>
        /// <returns>True if the item is found, false otherwise.</returns>
        public bool HasItemWithName(string itemName)
        {
            foreach (ItemData item in items)
            {
                if (item.itemName == itemName)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Returns a copy of the current inventory list.
        /// </summary>
        /// <returns>A new list containing all items in the inventory.</returns>
        public List<ItemData> GetAllItems()
        {
            return new List<ItemData>(items);
        }

        public void RemoveItem(ItemData item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                Debug.Log($"Removed item: {item.itemName} (ID: {item.itemID})");
            }
        }
    }
}