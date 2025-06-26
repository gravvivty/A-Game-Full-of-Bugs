using Project.Dialogue;
using Project.Helper;
using Project.Inventory;
using UnityEngine;

namespace Project.Interactable.Items
{
    /// <summary>
    /// Interactable item that can be picked up by the player and added to the inventory.
    /// Displays an icon based on its associated ItemData.
    /// </summary>
    public class PickupItem : Interactables
    {
        /// <summary>
        /// The data that defines this item's properties (name, ID, icon, etc.).
        /// </summary>
        [SerializeField] private ItemData itemData;
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Initializes the item by assigning its icon to the SpriteRenderer (if available).
        /// </summary>
        protected override void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Called when the player interacts with the item.
        /// Adds the item to the inventory and deactivates the GameObject.
        /// </summary>
        protected override void Interact()
        {

            if (spriteRenderer != null && itemData != null && itemData.icon != null)
            {
                spriteRenderer.sprite = itemData.icon;
            }
            DialogueManager.Instance.EndDialogue(); // End any ongoing dialogue when picking up an item
            InventoryManager.Instance.AddItem(itemData);

            // Reset Cursor after pickup
            CursorManager.Instance.SetNormalCursor();

            Destroy(gameObject);

            // Call the UpdateInventoryUI() method to update the UI when an item is picked up
            InventoryUI inventoryUI = FindFirstObjectByType<InventoryUI>(); // Find the InventoryUI component
            if (inventoryUI != null && inventoryUI.gameObject.activeInHierarchy)
            {
                inventoryUI.UpdateInventoryUI(); // Update the UI to show the new item
            }
            else
            {
                // Debug.LogWarning($"No ItemData assigned to {gameObject.name}");
                Debug.LogWarning("Inventory UI is not active or missing.");
            }
        }

        /// <summary>
        /// Gets the ItemData associated with this item.
        /// </summary>
        /// <returns>The ItemData object.</returns>
        public ItemData GetItemData()
        {
            return itemData;
        }
    }
}